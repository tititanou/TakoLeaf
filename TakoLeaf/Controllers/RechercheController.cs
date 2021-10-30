using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    [Authorize]
    public class RechercheController : Controller
    {


        private IdalProfil dal;


        public RechercheController()
        {
            this.dal = new DalProfil();

        }


        public IActionResult Index()
        {
            return View();
        }


        // TODO Pour avoir l'adherenrt connect�
        //int idA2 = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
           
       
            //int idmodele = voiture.ModeleId;
            //Modele modele = dal.ObtenirModeles().FirstOrDefault(m => m.Id == idmodele);
            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser, Voitures = voitures, Consumer = consumer, Modeles = modeles, Marques = marques, Amis = res };

                return View(uvm);
            

        }

        public IActionResult VisiteProfilProvider(int id)
        {
                Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
                CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(a => a.AdherentId == adherent.Id);

                Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
                List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();
                List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == provider.Id).ToList();

                int idA2 = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                DalRecherche dalR = new DalRecherche();
                bool res = dalR.EstAmi(id, idA2);

            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, Competence = competence, Provider = provider, Ressources = ressources, Amis = res };
            
            return View(pvm);
        }


    }
}
    



            



       

    