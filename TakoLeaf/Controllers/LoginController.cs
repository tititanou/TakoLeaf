﻿using Microsoft.AspNetCore.Authentication;
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
        private IdalProfil dalP;

        public LoginController()
        {
            this.dal = new DalLogin();
            this.dalP = new DalProfil();
        }

        public ActionResult Inscription()
        {
            List<string> roles = new List<string> { "Consumer", "Provider", "Hybride" };
            ViewBag.Roles = new SelectList(roles);
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
                    new Claim(ClaimTypes.Role, uvm.CompteUser.Role)
                };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                //UtilisateurViewModel uvm2 = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };
                if (uvm.CompteUser.Role.Equals("Provider"))
                {
                    dal.RoleIsProvider(compteUser);
                    return Redirect("/Login/InscriptionProvider");
                }

                else if (uvm.CompteUser.Role.Equals("Consumer"))
                {
                    dal.RoleIsConsumer(compteUser);
                    return Redirect("/Login/InscriptionConsumer");
                    //return RedirectToAction("InscriptionConsumer", "Login", new { uvm = uvm2 });
                }

                else if (uvm.CompteUser.Role.Equals("Provider et Consumer"))
                {
                    dal.RoleIsHybride(compteUser);

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
                Adherent adherent = dalP.ObtenirAdherents().Where(a => a.Id == id).FirstOrDefault();

                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Adherent.Nom),
                    new Claim(ClaimTypes.NameIdentifier, user.Adherent.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                if (user.Role.Equals("Consumer"))
                return Redirect("/ProfilUser/ProfilConsumer?id="+id);
                else if(user.Role.Equals("Provider"))
                return Redirect("/ProfilUser/ProfilProvider?id=" + id);

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
            return Redirect("/ProfilUser/ProfilConsumer?id=" + idadherent);
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
                listeSS.Add(new ProviderCheckBoxViewModel { Intitule = item.Intitule, EstSelectione = false, SsCateCompetenceId = item.Id });
            }
            
            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, ListSSC = listeSS };

            return View(pvm);
        }


        [HttpPost]
        public ActionResult InscriptionProvider(ProviderViewModel pvm)
        {

            Rib rib = dal.CreationRib(pvm.Rib.Titulaire, pvm.Rib.Iban, pvm.Rib.Banque);
            DalProfil dalProfil = new DalProfil();
            int idadherent = dalProfil.ObtenirAdherents().Last().Id;
            Provider provider = dal.CreationProvider(idadherent, rib.Id);
            
            List<int> listeId = new List<int>();
            List<double> listeT = new List<double>();
            List<string> listeN = new List<string>();

            for(int i = 0; i < pvm.ListSSC.Count; i++)
            {
                if(pvm.ListSSC[i].EstSelectione == true)
                {
                    listeId.Add(pvm.ListSSC[i].SsCateCompetenceId);
                    listeT.Add(pvm.ListSSC[i].TarifHoraire);
                    listeN.Add(pvm.ListSSC[i].Intitule);
                }
            }

            List<Competence> listeC = new List<Competence>();
            for(int i = 0; i < listeId.Count; i++)
            {
                Competence competence = dal.CreationCompetence(listeT[i], listeId[i], provider.Id, listeN[i]);
                listeC.Add(competence);
            }

            return Redirect("/ProfilUser/ProfilProvider?id=" + provider.AdherentId);
        }
    }
}
