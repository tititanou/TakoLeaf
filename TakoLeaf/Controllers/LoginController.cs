using Microsoft.AspNetCore.Authentication;
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

                //var radioButton1 = $('input[name=')
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, uvm.Adherent.Nom),
                    new Claim(ClaimTypes.NameIdentifier, uvm.Adherent.Id.ToString()),
                    new Claim(ClaimTypes.Email, uvm.CompteUser.Mail)
                };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return Redirect("/ProfilUser/Profil?id=" + idAdherent);

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


    }
}
