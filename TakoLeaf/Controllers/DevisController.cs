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
        private IWebHostEnvironment _env;


        public DevisController(IWebHostEnvironment env)
        {
            this.dal = new DalProfil();
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
                listD.Add(new DevisCheckBoxViewModel() { Intitule = item.NomSsCate, TarifHoraire = item.TarifHoraire, EstSelectione = false });
            }
            
            DemandeDevis demandeDevis = new DemandeDevis { DateDemande = DateTime.Now };

            DevisViewModel dvm = new DevisViewModel { DemandeDevis = demandeDevis ,Consumer = consumer, Provider = provider, Ressources = ressources, ListD = listD , Voiture = new Voiture()};

            return View(dvm);
        }    
    
    
    
    
    
    
    }
       
}
