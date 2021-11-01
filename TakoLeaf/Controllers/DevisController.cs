using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class DevisController : Controller
    {
        private IdalProfil dal;
        private IdalDevis dalD;
        private IWebHostEnvironment _env;


        public DevisController(IWebHostEnvironment env)
        {
            this.dal = new DalProfil();
            this.dalD = new DalDevis();
            this._env = env;

        }
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult DemandeDevis(int id)
        {
            int IdA = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == IdA);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == id);

            List<Voiture> voitures = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).ToList();
            List<string> modeles = new List<string>();
            foreach(var item in voitures)
            {
                modeles.Add(item.Modele.Nom);

            }
            
            ViewBag.Modeles = new SelectList(modeles);

            List<Ressource> ressources = dal.ObtenirRessources().Where(r => r.ProviderId == provider.Id).ToList();

            List<DevisCheckBoxViewModel> listD = new List<DevisCheckBoxViewModel>();

           foreach(Competence item in dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList())
            {
                listD.Add(new DevisCheckBoxViewModel() { Intitule = item.NomSsCate, TarifHoraire = item.TarifHoraire, EstSelectione = false, CompetenceId = item.Id });
            }

            List<DevisCheckBoxViewModel> listR = new List<DevisCheckBoxViewModel>();

            foreach (Ressource item in dal.ObtenirRessources().Where(c => c.ProviderId == provider.Id).ToList())
            {
                listR.Add(new DevisCheckBoxViewModel() { Intitule = item.Intitule, TarifHoraire = item.TarifJournalier, EstSelectione = false, RessourceId = item.Id});
            }

            DemandeDevis demandeDevis = new DemandeDevis { DateDemande = DateTime.Now };

            DevisViewModel dvm = new DevisViewModel { DemandeDevis = demandeDevis, Consumer = consumer, Provider = provider, Ressources = ressources, ListD = listD, ListR = listR, Voiture = new Voiture()};

            return View(dvm);
        }    

        [HttpPost]

        public IActionResult DemandeDevis(DevisViewModel dvm)
        {
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == dvm.Provider.Id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == dvm.Consumer.Id);
            Modele modele = dal.ObtenirModeles().FirstOrDefault(m => m.Nom.Equals(dvm.Voiture.Modele.Nom));
            Voiture voiture = dal.ObtenirVoiture().Where(v => v.ConsumerId == consumer.Id).FirstOrDefault(v => v.ModeleId == modele.Id);
            DemandeDevis demandeDevis = dalD.CreationDemandeDevis(consumer.Id, provider.Id, voiture.Id, dvm.DemandeDevis.DateDemande, dvm.DemandeDevis.DateDebutVoulue,dvm.DemandeDevis.Message);
            
            for(int i = 0; i< dvm.ListD.Count; i++)
            {
                if (dvm.ListD[i].EstSelectione == true)
                {
                    dalD.CreationListeDevisCompetence(dvm.ListD[i].CompetenceId, demandeDevis.Id);
                }
            }

            for (int i = 0; i < dvm.ListR.Count; i++)
            {
                if (dvm.ListR[i].EstSelectione == true)
                {
                    dalD.CreationListeDevisRessource(dvm.ListR[i].RessourceId, demandeDevis.Id);
                }
            }

            return Redirect("/ProfilUser/ProfilConsumer?id=" + consumer.AdherentId);
        }

        public IActionResult ListeDemandeDevis ()
        {
            int IdA = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == IdA);

            List<DemandeDevis> demandeDevis = dal.ObtenirDemandeDevis().Where(d => d.ProviderId == provider.Id).ToList();

            
            List<DemandeDevisViewModel> demandedevisF = new List<DemandeDevisViewModel>();

            for (int i=0; i<demandeDevis.Count; i++)
            {
                demandedevisF.Add(
                    new DemandeDevisViewModel {
                        DemandeDevis = demandeDevis[i],
                        ListeCompetences = dal.ObtenirCompetenceDevis().Where(c => c.DemandeDevisId == demandeDevis[i].Id).ToList(),
                        ListeRessources = dal.ObtenirRessourceDevis().Where(c => c.DemandeDevisId == demandeDevis[i].Id).ToList(),
                        Consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == demandeDevis[i].ConsumerId),
                        Voiture = dal.ObtenirVoiture().FirstOrDefault(v => v.Id == demandeDevis[i].VoitureId)
                    });
                //

            }

            DevisViewModel dvm = new DevisViewModel { Provider = provider,ListeDemandeDevis = demandedevisF };
            return View(dvm);

        }

        public IActionResult EmmetreDevis(int id)
        {
            DemandeDevis demande = dal.ObtenirDemandeDevis().FirstOrDefault(d => d.Id == id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == demande.ProviderId);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == demande.ConsumerId);
            Voiture voiture = dal.ObtenirVoiture().FirstOrDefault(v => v.Id == demande.VoitureId);

            List<DemandeDevisListeCompetence> listC = dal.ObtenirCompetenceDevis().Where(l => l.DemandeDevisId == demande.Id).ToList();
            List<DemandeDevisListeRessource> listR = dal.ObtenirRessourceDevis().Where(l => l.DemandeDevisId == demande.Id).ToList();

            double tarif = 0;

            foreach(var item in listC)
            {
                Competence competence = dal.ObtenirCompetences().FirstOrDefault(c => c.Id == item.CompetenceId);
                tarif = tarif + competence.TarifHoraire;
            }

            foreach(var item in listR)
            {
                Ressource ressource = dal.ObtenirRessources().FirstOrDefault(r => r.Id == item.RessourceId);
                tarif = tarif + ressource.TarifJournalier;
            }

            Devis devis = new Devis { 
                Tarif = tarif, 
                DateEmission = DateTime.Now, 
                LieuPresta = provider.Adherent.Adresse,
                DemandeDevisId = demande.Id
                                
            };

            DevisViewModel dvm = new DevisViewModel { Consumer = consumer, Provider = provider, Voiture = voiture, DemandeDevis = demande, Devis = devis, Tarif = tarif, ListeCompetencesDevis = listC, ListeRessourcesDevis = listR };

            return View(dvm);
        }

        [HttpPost]

        public IActionResult EmmetreDevis(DevisViewModel dvm)
        {
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == dvm.Consumer.Id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.Id == dvm.Provider.Id);
            Voiture voiture = dal.ObtenirVoiture().FirstOrDefault(v => v.Id == dvm.Voiture.Id);
            DemandeDevis demandeDevis = dal.ObtenirDemandeDevis().FirstOrDefault(d => d.Id == dvm.DemandeDevis.Id);
            int adresse = provider.Adherent.Adresse.Id;

            dalD.CreationDevis(provider.Id, consumer.Id, voiture.Id, demandeDevis.Id, dvm.Devis.DateEmission, dvm.Devis.DateDebut, dvm.Devis.DateFin, dvm.Devis.Tarif, dvm.Devis.DescriptionPresta, adresse);

            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);


        }

        public IActionResult AccepterDevis()
        {

            int IdA = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == IdA);

            List<Devis> devis = dal.ObtenirDevis().Where(d => d.ConsumerId == consumer.Id).ToList();

            
            return View(devis);


        }

        [HttpPost]
        public IActionResult AccepterDevis(string numero)
        {
            Devis devis = dal.ObtenirDevis().FirstOrDefault(d => d.NumeroDevis.Equals(numero));
            dalD.CreationPrestation(devis);
            return Redirect("/ProfilUser/ProfilConsumer?id=" + devis.Consumer.AdherentId);
        }

        [HttpPost]

        public IActionResult RefuserDevis(string numero)
        {
            Devis devis = dal.ObtenirDevis().FirstOrDefault(d => d.NumeroDevis.Equals(numero));
            dalD.CreationPrestationRefusee(devis);
            return Redirect("/ProfilUser/ProfilConsumer?id=" + devis.Consumer.AdherentId);
        }





    }
       
}
