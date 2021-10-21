using Microsoft.AspNetCore.Mvc;
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
                Adherent aderent = dal.CreationAdherent(uvm.Adherent.Nom, uvm.Adherent.Prenom, uvm.Adherent.Date_naissance, uvm.Adherent.Adresse, uvm.Adherent.Telephone);
                int idAdherent = aderent.Id;
                CompteUser compteUser = dal.CreationCompte(uvm.CompteUser.Mail, uvm.CompteUser.MotDePasse, uvm.CompteUser.Avatar, uvm.CompteUser.Description, idAdherent);

                // TODO A voir pour le TOKEN
                //var userClaims = new List<Claim>()
                //{
                //    new Claim(Claim)
                //}

                return View("InscriptionReussie");

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

       
        
    }
}
