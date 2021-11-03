using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class PrestationController : Controller
    {
        private IdalProfil dal;
        


        public PrestationController()
        {
            this.dal = new DalProfil();
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrestationsEnCours(int id)
        {
            CompteUser compte = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);

            if (compte.Role.Equals("Consumer"))
            {
                Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
                List<Prestation> prestations = dal.ObtenirToutesLesPrestations().Where(p => p.ConsumerId == consumer.Id).Where(p => p.EtatPresta!= Prestation.Etat.Valide).ToList();

                return View(prestations);
            }

            else if (compte.Role.Equals("Provider"))
            {
                Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
                List<Prestation> prestations = dal.ObtenirToutesLesPrestations().Where(p => p.ProviderId == provider.Id).Where(p => p.EtatPresta != Prestation.Etat.Valide).ToList();
                return View(prestations);
            }

            return View();
        }

        [HttpPost]

        public IActionResult ValiderPrestation(int id)
        {
            Prestation prestation = dal.ObtenirToutesLesPrestations().FirstOrDefault(p => p.Id == id);
            DalPrestation dalP = new DalPrestation();
            dalP.ChangerEtatPrestation(prestation.Id);
            dalP.AjoutHistorique(prestation);

            return Redirect("/Prestation/LaisserNote?id=" + prestation.Id);
        }

        public IActionResult LaisserNote(int id)
        {
            Prestation prestation = dal.ObtenirToutesLesPrestations().FirstOrDefault(p => p.Id == id);
            AvisViewModel avm = new AvisViewModel { Prestation = prestation };
            return View(avm);

        }

        [HttpPost]
        public IActionResult LaisserNote(AvisViewModel avm)
        {
            DalPrestation dalPrestation = new DalPrestation();
            dalPrestation.CreationAvis(avm.Prestation.Consumer.Id, avm.Prestation.Provider.Id, avm.Avis.Note, avm.Avis.Contenu, avm.Prestation.Id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.Id == avm.Prestation.Consumer.Id);

            return Redirect("/Prestation/Historique?id=" + consumer.AdherentId);


        }
        //return Redirect("/ProfilUser/ProfilConsumer?id=" + prestation.Consumer.AdherentId);

        public IActionResult Historique(int id)
        {
            List<HistoriquePresta> historiquePrestas = dal.ObtenirHistorique().Where(h => h.HistoriqueId == id).ToList();
            List<Avis> liste = new List<Avis>();
            CompteUser compte = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            foreach(HistoriquePresta item in historiquePrestas)
            {
                liste.Add(dal.ObtenirAvis().FirstOrDefault(a => a.PrestationId == item.PrestationId));
            }
            HistoriqueViewModel hvm = new HistoriqueViewModel { Avis = liste, HistoriquePrestas = historiquePrestas, Compte = compte };
            return View(hvm);
        }
    }
}
