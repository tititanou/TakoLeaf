using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    
    public class RechercheController : Controller
    {


        private IdalProfil dal;
        private IDalRecherche dalRecherche;


        public RechercheController()
        {
            this.dal = new DalProfil();
            this.dalRecherche = new DalRecherche();
        }


        public IActionResult Recherche()
        {
            List<string> choix = new List<string>() { "Un utilisateur", "Un service", "Une ressource" };
            ViewBag.Choix = new SelectList(choix);
            List<string> cateRessources = new List<string>();
            foreach (string item in Enum.GetNames(typeof(CateRessource)))
            {
                cateRessources.Add(item);
            }
            ViewBag.Ressources = new SelectList(cateRessources);


            List<Adherent> adherents = this.dal.ObtenirAdherents();
            List<string> prenoms = new List<string>();
            List<string> noms = new List<string>();
            foreach(Adherent adherent in adherents)
            {
                if(prenoms.Count() == 0)
                {
                    prenoms.Add(adherent.Prenom);
                }
                else
                {
                    if (!prenoms.Contains(adherent.Prenom))
                    {
                        prenoms.Add(adherent.Prenom);
                    }
                }
                if (noms.Count() == 0)
                {
                    noms.Add(adherent.Nom);
                }
                else
                {
                    if (!noms.Contains(adherent.Nom))
                    {
                        noms.Add(adherent.Nom);
                    }
                }
            }
            noms.Sort();
            prenoms.Sort();
            ViewBag.Noms = new List<string>(noms);
            ViewBag.Prenoms = new List<string>(prenoms);

             var model = new RechercheViewModel()
             {
                 SsCateList = new List<SelectListItem>(),
                 CodePostauxList = new List<SelectListItem>()
             };

             var ssCateComp = this.dalRecherche.RechercheCompetence();
             var adresses = this.dalRecherche.RechercheCodePostal();

            foreach(var adresse in adresses)
            {
                var optionGroup = new SelectListGroup() { Name = adresse.Key.ToString() };
                foreach(var codePostal in adresse.Value)
                {
                    model.CodePostauxList.Add(new SelectListItem() { Value = codePostal.ToString(), Text = codePostal.ToString(), Group = optionGroup });
                }
            }

             foreach (var cateGroup in ssCateComp)
             {
                 var optionGroup = new SelectListGroup() { Name = cateGroup.Key };
                 foreach (var ssCateCompetence in cateGroup.Value)
                 {
                     model.SsCateList.Add(new SelectListItem() { Value = ssCateCompetence.Id.ToString(), Text = ssCateCompetence.Intitule, Group = optionGroup });
                 }
             }
             return View(model);
        }

        [HttpPost]
        public ActionResult Recherche(string Choix, int Adresse, string Prenom, string Nom, int Competence, string Ressource, string Input)
        {
            List<Adherent> resultats = this.dalRecherche.RechercheAdherent(Choix, Adresse, Nom, Prenom, Competence, Ressource, Input);
            return View("AfficherProfils", resultats);
        }

        public ActionResult AfficherProfils(List<Adherent> Adhs)
        {

            return View(Adhs);
        }
        // TODO Pour avoir l'adherenrt connect?
        //int idA2 = Int32.Parse(User.FindFirst(PClaimTypes.NameIdentifier).Value);
        //

        public IActionResult VisiteProfil(int id) // idAdherent-+
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == adherent.Id);
            if(compteUser.Role.Equals("Consumer"))
            {
                return Redirect("/Recherche/VisiteProfilConsumer?id=" + id);
            }

            else if(compteUser.Role.Equals("Provider"))
            {
                return Redirect("/Recherche/VisiteProfilProvider?id=" + id);
            }

            return View();

        }

        public IActionResult VisiteProfilConsumer(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(a => a.AdherentId == adherent.Id);
           
                Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
                List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).OrderBy(v => v.Id).ToList();
                //int idcarte = consumer.CarteId;
                //Carte carte = dal.ObtenirCartes().FirstOrDefault(c => c.Id == idcarte);

                List<string> modeles = new List<string>();
                for (int i = 0; i < voitures.Count; i++)
                {
                    int idMo = voitures[i].ModeleId;
                    modeles.Add(dal.ObtenirModeles().FirstOrDefault(m => m.Id == idMo).Nom);
                }

                List<string> marques = new List<string>();
                for (int i = 0; i < modeles.Count; i++)
                {
                    int idM = dal.ObtenirModeles().Where(m => m.Nom.Equals(modeles[i])).FirstOrDefault().MarqueId;
                    marques.Add(dal.ObtenirMarques().Where(m => m.Id == idM).FirstOrDefault().Nom);
                }

            
            int idA2 = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DalRecherche dalR = new DalRecherche();
            bool res = dalR.EstAmi(id, idA2);
            List<Avis> avis = dal.ObtenirAvis().Where(a => a.ConsumerId == consumer.Id).ToList();


            //int idmodele = voiture.ModeleId;
            //Modele modele = dal.ObtenirModeles().FirstOrDefault(m => m.Id == idmodele);
            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, Avis = avis, CompteUser = compteUser, Voitures = voitures, Consumer = consumer, Modeles = modeles, Marques = marques, Amis = res };

                return View(uvm);
        }

        public IActionResult VisiteProfilProvider(int id)
        {
                Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
                CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(a => a.AdherentId == adherent.Id);

                Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
                List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();
                List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == provider.Id).ToList();
                List<Avis> avis = dal.ObtenirAvis().Where(a => a.Provider.AdherentId == adherent.Id).ToList();
                int idA2 = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                DalRecherche dalR = new DalRecherche();
                bool res = dalR.EstAmi(id, idA2);

            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, Competence = competence, Provider = provider, Ressources = ressources, Amis = res, Avis = avis };
            
            return View(pvm);
        }


    }
}
    



            



       

    