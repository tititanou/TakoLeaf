using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class ProfilUserController : Controller
    {
        private IdalProfil dal;
        private IWebHostEnvironment _env;


        public ProfilUserController(IWebHostEnvironment env)
        {
            this.dal = new DalProfil();
            this._env = env;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        // PROFILS

        public IActionResult ProfilConsumer(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).OrderBy(v => v.Id).ToList();
            //int idcarte = consumer.CarteId;
            //Carte carte = dal.ObtenirCartes().FirstOrDefault(c => c.Id == idcarte);
            
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
            List<Avis> avis = dal.ObtenirAvis().Where(a => a.ConsumerId == consumer.Id).ToList();
            //
            List<Provider> providers = new List<Provider>();
            foreach (Avis avi in avis)
            {
                Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == avi.ProviderId);
                providers.Add(provider);
            }
           //
            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser, Avis =avis, Voitures = voitures, Consumer = consumer, Modeles = modeles , Marques = marques, Providers = providers };

            return View(uvm);
        }

        public IActionResult ProfilProvider(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
            List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();
            List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == provider.Id).ToList();
            List<Avis> avis = dal.ObtenirAvis().Where(a => a.ProviderId == provider.Id).ToList();

            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, Competence = competence, Provider = provider, Ressources = ressources, Avis = avis };

            return View(pvm);
        }    

        public IActionResult VoirProfil()
        {
            int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
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

        // MODIFICATION INFORMATIONS ADHERANT / COMPTE

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
                int idA = adherent.AdresseId;
                dal.ModifierAdresse(idA, adherent.Adresse.Rue, adherent.Adresse.CodePostal, adherent.Adresse.Ville);
                dal.ModifierInfosAdherent(adherent.Id, adherent.Nom, adherent.Prenom, adherent.Date_naissance, adherent.Telephone);
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
        public IActionResult ModifCompte(CompteUser compteUser, IFormFile fileToUpload)
        {
            using(IdalProfil dal = new DalProfil())
            {
                dal.ModifierCompteUser(compteUser.Mail, compteUser.MotDePasse, fileToUpload.FileName, compteUser.Description);
                int id = compteUser.AdherentId;

                if (fileToUpload.Length > 0)
                {
                    string path = _env.WebRootPath + "/Avatar/" + fileToUpload.FileName;
                    FileStream stream = new FileStream(path, FileMode.Create);
                    fileToUpload.CopyTo(stream);
                }

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
        

        // COMPETENCES

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


        public IActionResult SupprimerCompetence(int id)
        {
            Competence competence = dal.ObtenirCompetences().FirstOrDefault(c => c.Id == id);
            int idP = competence.ProviderId;
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == idP);
            DalProfil dalProfil = new DalProfil();
            dalProfil.SupprimerCompetence(competence);

            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);
        }



        // RESSOURCES

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
            DalLogin dalLogin = new DalLogin();
            Adresse adresse = dalLogin.CreationAdresse(pvm.Ressource.Adresse.Rue, pvm.Ressource.Adresse.CodePostal, pvm.Ressource.Adresse.Ville);
            Ressource ressource = dal.AjouterRessource(pvm.Provider.Id, pvm.Ressource.Intitule, pvm.Ressource.Categorie, pvm.Ressource.TarifJournalier, adresse.Id);
            return Redirect("/ProfilUser/ProfilProvider?id=" + pvm.Provider.AdherentId);
        }

        public IActionResult ModifierRessource(int id)
        {
            List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == id).ToList();
            List<CateRessource> cateRessources = new List<CateRessource> { CateRessource.OUTIL, CateRessource.OUTIL_SPECIALISE, CateRessource.LOCAL_GARAGE, CateRessource.TERRAIN, CateRessource.REMORQUE, CateRessource.PONT_ELEVATEUR };
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == id);
            ViewBag.Cate = new SelectList(cateRessources);

            ProviderViewModel pvm = new ProviderViewModel { Provider = provider, Ressources = ressources };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult ModifierRessource(ProviderViewModel pvm)
        {
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == pvm.Provider.Id);

            for(int i =0; i < pvm.Ressources.Count; i++)
            {
                dal.ModifierRessource(pvm.Ressources[i].Id, pvm.Ressources[i].Intitule, pvm.Ressources[i].Categorie, pvm.Ressources[i].TarifJournalier, pvm.Ressources[i].Adresse.Rue, pvm.Ressources[i].Adresse.CodePostal, pvm.Ressources[i].Adresse.Ville);
            }


            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);
        }


        public IActionResult SupprimerRessource(int id)
        {
            Ressource ressource = dal.ObtenirRessources().FirstOrDefault(r => r.Id == id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == ressource.ProviderId);
            dal.SupprimerRessource(ressource);


            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);
        }

        // VEHICULES 

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


        public IActionResult ModifVoiture(int id)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == id);
            List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).OrderBy(v => v.Id).ToList();
            

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

            UtilisateurViewModel uvm = new UtilisateurViewModel { Consumer = consumer, Voitures = voitures, Modeles = modeles, Marques = marques };

            return View(uvm);

        }

        [HttpPost]

        public IActionResult ModifVoiture (UtilisateurViewModel uvm)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == uvm.Consumer.Id);
            List<Voiture> voitures = uvm.Voitures;
            using(DalProfil dalProfil = new DalProfil())
            {
                
                for (int i=0; i<voitures.Count; i++)
                {
                    int idmodele = dal.ObtenirModeles().Where(v => v.Nom == uvm.Modeles[i]).FirstOrDefault().Id;
                    dal.ModifierVoiture(uvm.Voitures[i].Id, uvm.Voitures[i].Immatriculation, uvm.Voitures[i].Titulaire, uvm.Voitures[i].Carburant, uvm.Voitures[i].Annee, idmodele);

                }
            }

            return Redirect("/ProfilUser/ProfilConsumer?id=" + consumer.AdherentId);


        }

        
        public IActionResult SuppressionVoiture(int id)
        {
            Voiture voiture = dal.ObtenirVoiture().FirstOrDefault(v => v.Id == id);
            int idC = voiture.ConsumerId;
            DalProfil dalProfil = new DalProfil();
            dalProfil.SupprimerVoiture(voiture);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == idC);
            return Redirect("/ProfilUser/ProfilConsumer?id=" + consumer.AdherentId);

        }

        // CARTE 
        public IActionResult AjouterCarte(int id)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            UtilisateurViewModel uvm = new UtilisateurViewModel { Consumer = consumer };
            return View(uvm);
        }

        [HttpPost]
        public IActionResult AjouterCarte(UtilisateurViewModel uvm)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == uvm.Consumer.Id);
            DalLogin dalL = new DalLogin();
            Carte carte = dalL.CreationCarte(consumer.Id, uvm.Carte.Titulaire, uvm.Carte.NumeroCarte, uvm.Carte.ExpirDate, uvm.Carte.Crypto);

            return Redirect("/ProfilUser/ModifierCarte?id=" + consumer.AdherentId);
        }



        public IActionResult ModifierCarte(int id)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            List<Carte> cartes = dal.ObtenirCartes().Where(c => c.ConsumerId == consumer.Id).ToList();

            UtilisateurViewModel uvm = new UtilisateurViewModel { Consumer = consumer, Cartes = cartes };


            return View(uvm);
        }

        [HttpPost]
        public IActionResult ModifierCarte(UtilisateurViewModel uvm)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == uvm.Consumer.Id);
            for(int i =0; i<uvm.Cartes.Count;i++) 
            {
                dal.ModifierCarte(uvm.Cartes[i].Id, uvm.Cartes[i].Titulaire, uvm.Cartes[i].NumeroCarte, uvm.Cartes[i].ExpirDate, uvm.Cartes[i].Crypto);
            }

            return Redirect("/ProfilUser/ModifierCarte?id=" + consumer.AdherentId);
        }

        public IActionResult SupprimerCarte(int id)
        {

            Carte carte = dal.ObtenirCartes().FirstOrDefault(c => c.Id == id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == carte.ConsumerId);
            dal.SupprimerCarte(carte);

            return Redirect("/ProfilUser/ModifierCarte?id=" + consumer.AdherentId);
        }

        [HttpPost]

        public IActionResult Suivre(int id)
        {
            Adherent adherent1 = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == adherent1.Id);
            int idA2 = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Adherent adherent2 = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == idA2);
            dal.AjoutAmis(adherent1, adherent2);

            if (compteUser.Role.Equals("Consumer"))
            {
                return Redirect("/Recherche/VisiteProfilConsumer?id=" + id);
            }

            else if (compteUser.Role.Equals("Provider"))
            {
                return Redirect("/Recherche/VisiteProfilProvider?id=" + id);
            }

            return View();
        }

        // HISTORIQUE

        public IActionResult Modif(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            List<Carte> cartes = dal.ObtenirCartes().Where(c => c.ConsumerId == consumer.Id).ToList();
            List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).OrderBy(v => v.Id).ToList();
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

            UtilisateurViewModel uvm = new UtilisateurViewModel { Consumer = consumer,Adherent = adherent, Cartes = cartes, Voitures = voitures };

            return View(uvm);
        }
       
    }   
}
