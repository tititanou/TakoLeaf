using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class ProfilUserController : Controller
    {
        private IdalProfil dal;
     

        public ProfilUserController()
        {
            this.dal = new DalProfil();
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfilConsumer(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).ToList();
            int idcarte = consumer.CarteId;
            Carte carte = dal.ObtenirCartes().FirstOrDefault(c => c.Id == idcarte);
            
            List<string> modeles = new List<string>();
            for(int i =0; i<voitures.Count; i++)
            {
                int idMo = voitures[i].ModeleId;
                modeles.Add(dal.ObtenirModeles().FirstOrDefault(m => m.Id == idMo).Nom);
            }

            List<string> marques = new List<string>();
            for(int i=0; i< modeles.Count;i++)
            {
                int idM = dal.ObtenirModeles().Where(m => m.Nom.Equals(modeles[i])).FirstOrDefault().MarqueId;
                marques.Add(dal.ObtenirMarques().Where(m => m.Id == idM).FirstOrDefault().Nom);
            }

            //int idmodele = voiture.ModeleId;
            //Modele modele = dal.ObtenirModeles().FirstOrDefault(m => m.Id == idmodele);
            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser, Voitures = voitures, Carte = carte, Consumer = consumer, Modeles = modeles , Marques = marques };

            return View(uvm);
        }

        public IActionResult ProfilProvider(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
            List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();
            List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == provider.Id).ToList();

            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, Competence = competence, Provider = provider, Ressources = ressources };

            return View(pvm);
        }    

        public IActionResult ModifInfosAdherent(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            return View(adherent);
        }

        [HttpPost]
        public IActionResult ModifInfosAdherent(Adherent adherent)
        {
            using(DalProfil dal = new DalProfil())
            {
                dal.ModifierInfosAdherent(adherent.Id, adherent.Nom, adherent.Prenom, adherent.Date_naissance, adherent.Adresse, adherent.Telephone);
                int id = adherent.Id;
                CompteUser compteUser = dal.ObtenirCompteUser().Where(c => c.AdherentId == id).FirstOrDefault();
                if(compteUser.Role.Equals("Consumer"))
                {
                    return Redirect("/ProfilUser/ProfilConsumer?id=" + id);
                }
                
                else if (compteUser.Role.Equals("Provider"))
                {
                    return Redirect("/ProfilUser/ProfilProvider?id=" + id);
                }

                return View();
               
            }
            
        }

        public IActionResult ModifCompte(int id)
        {
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);

            return View(compteUser);
        }

        [HttpPost]
        public IActionResult ModifCompte(CompteUser compteUser)
        {
            using(IdalProfil dal = new DalProfil())
            {
                dal.ModifierCompteUser(compteUser.Mail, compteUser.MotDePasse, compteUser.Avatar, compteUser.Description);
                int id = compteUser.AdherentId;
                Adherent adherent = dal.ObtenirAdherents().Where(a => a.Id == id).FirstOrDefault();
                if (compteUser.Role.Equals("Consumer"))
                {
                    return Redirect("/ProfilUser/ProfilConsumer?id=" + id);
                }

                else if (compteUser.Role.Equals("Provider"))
                {
                    return Redirect("/ProfilUser/ProfilProvider?id=" + id);
                }

                return View();
            }
            
        }

        //TODO faire la methode et voir sur la vue InscriptionProvider pour ajouter les Competences
        
        public IActionResult ModifCompetence (int id)
        {
            List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == id).ToList();
            return View(competence);
        }


        [HttpPost]
        public IActionResult ModifCompetence (List<Competence> competence)
        {
            int idP = competence[0].ProviderId;
            Provider provider = dal.ObtenirProviders().Where(p => p.Id == idP).FirstOrDefault();
            int idA = provider.AdherentId;
            using(DalProfil dal = new DalProfil())
            {
                for(int i = 0; i<competence.Count;i++)
                {
                    int id = competence[i].Id;
                    double tarif = competence[i].TarifHoraire;
                    dal.ModifierCompetence(id, tarif);
                }
            }


            return Redirect("/ProfilUser/ProfilProvider?id=" + idA);
        }


        public IActionResult AjoutCompetence(int id)
        {
            DalProfil dal = new DalProfil();
            Provider provider = dal.ObtenirProviders().Where(p => p.Id == id).FirstOrDefault();
            List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();
            bool res = false;

            //UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };

            List<ProviderCheckBoxViewModel> listeSS = new List<ProviderCheckBoxViewModel>();
            foreach (SsCateCompetence item in dal.ObtenirSSCompetences().ToList().OrderBy(c => c.Id))
            {
                foreach(Competence item2 in competence)
                {
                    if (item2.SsCateCompetenceId == item.Id)
                    {
                        res = true;
                        break;
                    }
                    else
                        res = false;                       
                }
                
                if(res != true)
                {
                listeSS.Add(new ProviderCheckBoxViewModel { Intitule = item.Intitule, EstSelectione = false, SsCateCompetenceId = item.Id });
                }
               
            }

            
            ProviderViewModel pvm = new ProviderViewModel { Provider = provider, ListSSC = listeSS };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult AjoutCompetence (ProviderViewModel pvm)
        {
            DalProfil dalProfil = new DalProfil();
            DalLogin dalLogin = new DalLogin();
            int idP = pvm.Provider.Id;
            Provider provider = dal.ObtenirProviders().Where(p => p.Id == idP).FirstOrDefault();
            


            List<int> listeId = new List<int>();
            List<double> listeT = new List<double>();
            List<string> listeN = new List<string>();

            for (int i = 0; i < pvm.ListSSC.Count; i++)
            {
                if (pvm.ListSSC[i].EstSelectione == true)
                {
                    listeId.Add(pvm.ListSSC[i].SsCateCompetenceId);
                    listeT.Add(pvm.ListSSC[i].TarifHoraire);
                    listeN.Add(pvm.ListSSC[i].Intitule);
                }
            }

            List<Competence> listeC = new List<Competence>();
            for (int i = 0; i < listeId.Count; i++)
            {
                Competence competence = dalLogin.CreationCompetence(listeT[i], listeId[i], idP, listeN[i]);
                listeC.Add(competence);
            }

            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);
        }

        public IActionResult AjoutRessource(int id)
        {
            Provider provider = dal.ObtenirProviders().Where(p => p.Id == id).FirstOrDefault();
            List<CateRessource> cateRessources = new List<CateRessource> { CateRessource.OUTIL, CateRessource.OUTIL_SPECIALISE, CateRessource.LOCAL_GARAGE, CateRessource.TERRAIN, CateRessource.REMORQUE, CateRessource.PONT_ELEVATEUR };
            ViewBag.Cate = new SelectList(cateRessources);
            ProviderViewModel pvm = new ProviderViewModel { Provider = provider };

            return View(pvm);
        }

        [HttpPost]
        public IActionResult AjoutRessource(ProviderViewModel pvm)
        {
            Ressource ressource = dal.AjouterRessource(pvm.Provider.Id, pvm.Ressource.Intitule, pvm.Ressource.Categorie, pvm.Ressource.TarifJournalier, pvm.Ressource.Adresse);
            return Redirect("/ProfilUser/ProfilProvider?id=" + pvm.Provider.AdherentId);
        }

        public IActionResult AjoutVehicule(int id)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == id);

            // Liste de carburants
            List<Carburant> carburants = new List<Carburant>();
            foreach (Carburant item in Enum.GetValues(typeof(Carburant)))
            {
                carburants.Add(item);
            }
            ViewBag.SelectList = new SelectList(carburants);

            // Liste de modeles
            List<string> modeles = new List<string>();
            foreach (Modele item in dal.ObtenirModeles().ToList().OrderBy(p => p.Nom))
            {

                modeles.Add(item.Nom);
            }
            ViewBag.Modele = new SelectList(modeles);

            // Liste de marques
            List<string> marques = new List<string>();
            foreach (Marque item in dal.ObtenirMarques().ToList().OrderBy(p => p.Nom))
            {
                marques.Add(item.Nom);
            }
            ViewBag.Marques = new SelectList(marques);

            UtilisateurViewModel uvm = new UtilisateurViewModel { Consumer = consumer };

            return View(uvm);
        }

        [HttpPost]
        public IActionResult AjoutVehicule(UtilisateurViewModel uvm)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == uvm.Consumer.Id);
            DalLogin dalL = new DalLogin();
            int idmodele = dal.ObtenirModeles().Where(v => v.Nom == uvm.Modele.Nom).FirstOrDefault().Id;
            Voiture voiture = dalL.CreationVoiture(uvm.Voiture.Immatriculation, uvm.Voiture.Titulaire, uvm.Voiture.Carburant, uvm.Voiture.Annee, idmodele , consumer.Id);

            return Redirect("/ProfilUser/ProfilConsumer?id=" + consumer.AdherentId);
        }

    }   
}
