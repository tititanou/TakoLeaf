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
    public class AdminController : Controller
    {
        private IDalAdmin dal;


        public AdminController()
        {
            this.dal = new DalAdmin();
        }

        public IActionResult Index()
        {

            AdminViewModel viewModel = new AdminViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                viewModel.Admin = dal.ObtenirAdmin(userId);
                return Redirect("/home/index");
            }
            return View(viewModel);

        }

        public IActionResult Dashboard()
        {
            List<UtilisateurViewModel> liste = dal.ObtenirTousLesAdherentsEtComptes();

            return View(liste);
        }

        public IActionResult ValiderProfil(int id)
        {
            dal.ValidationProfil(id);
            return RedirectToAction("Dashboard");
        }

        //[HttpPost]
        //public IActionResult DashBoard()
        //{

        //}


        // TODO Faire l'authentification

        //[HttpPost]
        //public ActionResult Index(AdminViewModel viewModel, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Admin admin = dal.Authentifier(viewModel.Utilisateur.Prenom, viewModel.Utilisateur.Password);
        //        if (utilisateur != null)
        //        {
        //            var userClaims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.Name, utilisateur.Prenom),
        //                new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
        //                new Claim(ClaimTypes.Role, utilisateur.Role),
        //            };

        //            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

        //            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
        //            HttpContext.SignInAsync(userPrincipal);



        //            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        //                return Redirect(returnUrl);
        //            return Redirect("/");
        //        }
        //        ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
        //    }
        //    return View(viewModel);
        //}


    }
}
