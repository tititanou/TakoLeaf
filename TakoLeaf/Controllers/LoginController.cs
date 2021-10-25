using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
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
    public class LoginController : Controller
    {
        private IdalLogin dal;

        public LoginController()
        {
            this.dal = new DalLogin();
        }

        public ActionResult Inscription()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Inscription(UtilisateurViewModel uvm)
        {
            if (ModelState.IsValid) //TODO a voir pour le modelState et les Regex
            {
                Adherent adherent = dal.CreationAdherent(uvm.Adherent.Nom, uvm.Adherent.Prenom, uvm.Adherent.Date_naissance, uvm.Adherent.Adresse, uvm.Adherent.Telephone);
                int idAdherent = adherent.Id;
                CompteUser compteUser = dal.CreationCompte(uvm.CompteUser.Mail, uvm.CompteUser.MotDePasse, uvm.CompteUser.Avatar, uvm.CompteUser.Description, idAdherent);

                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, uvm.Adherent.Nom),
                    new Claim(ClaimTypes.NameIdentifier, uvm.Adherent.Id.ToString()),
                    new Claim(ClaimTypes.Email, uvm.CompteUser.Mail)
                };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                //UtilisateurViewModel uvm2 = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };
                if (uvm.Adherent.IsProvider == true)
                {
                    dal.IsProviderChecked(adherent);
                    return Redirect("/Login/InscriptionProvider");
                }

                else if (uvm.Adherent.IsConsumer == true)
                {
                    dal.IsConsumerChecked(adherent);
                    return Redirect("/Login/InscriptionConsumer");
                    //return RedirectToAction("InscriptionConsumer", "Login", new { uvm = uvm2 });
                }

                else if (uvm.Adherent.IsConsumer == true && uvm.Adherent.IsProvider == true)
                {


                }

            }

            return View(uvm);
        }
        public ActionResult InscriptionReussie()
        {
           return View();
        }

        public ActionResult Connexion()
        {
            return View();

        }

        [HttpPost]

        public ActionResult Connexion(CompteUser compteUser)
        {
            CompteUser user = dal.Authentifier(compteUser.Mail, compteUser.MotDePasse);
            if(user != null)
            {
                int id = user.AdherentId;
                return Redirect("/ProfilUser/Profil?id="+id);
                               
            }
            return View();
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Login/Connexion");
        }

        public ActionResult InscriptionConsumer()
        {
            DalProfil dal = new DalProfil();

            Adherent adherent = dal.ObtenirAdherents().Last();
            CompteUser compteUser = dal.ObtenirCompteUser().Where(c => c.AdherentId == adherent.Id).FirstOrDefault();

            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };


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


            return View(uvm);
        }

        [HttpPost]
        public ActionResult InscriptionConsumer(UtilisateurViewModel uvm)
        {
            Carte carte = dal.CreationCarte(uvm.Carte.Titulaire, uvm.Carte.NumeroCarte, uvm.Carte.ExpirDate, uvm.Carte.Crypto);
            int idcarte = carte.Id;
            DalProfil dalProfil = new DalProfil();
            int idadherent = dalProfil.ObtenirAdherents().Last().Id; 
            Consumer consumer = dal.CreationConsumer(idadherent, idcarte);
            int idmodele = dalProfil.ObtenirModeles().Where(v => v.Nom == uvm.Modele.Nom).FirstOrDefault().Id;
            Voiture voiture = dal.CreationVoiture(uvm.Voiture.Immatriculation, uvm.Voiture.Titulaire, uvm.Voiture.Carburant, uvm.Voiture.Annee, idmodele, consumer.Id);
            return Redirect("/ProfilUser/Profil?id=" + idadherent);
        }


        
        public ActionResult InscriptionProvider()
        {
            DalProfil dal = new DalProfil();
            Adherent adherent = dal.ObtenirAdherents().Last();
            CompteUser compteUser = dal.ObtenirCompteUser().Where(c => c.AdherentId == adherent.Id).FirstOrDefault();

            //UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };

            List<ProviderCheckBoxViewModel> listeSS = new List<ProviderCheckBoxViewModel>();
            foreach(SsCateCompetence item in dal.ObtenirSSCompetences().ToList().OrderBy(c => c.Id))
            {
                listeSS.Add(new ProviderCheckBoxViewModel { Intitule = item.Intitule, EstSelectione = false });
            }
            
            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, ListSSC = listeSS };

            return View(pvm);
        }

    }
}
