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

        public ActionResult Index()
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

        public ActionResult Dashboard()
        {
            List<UtilisateurViewModel> liste = dal.ObtenirTousLesAdherentsEtComptes();

            return View(liste);
        }

        public ActionResult ValiderProfil(int id)
        {
            dal.ChangerEtatProfil(id, 0);
            return RedirectToAction("Dashboard");
        }

        public ActionResult BloquerProfil(int id)
        {
            dal.ChangerEtatProfil(id, 1);
            return RedirectToAction("Dashboard");
        }

        public ActionResult DebloquerProfil(int id)
        {
            dal.ChangerEtatProfil(id, 2);
            return RedirectToAction("Dashboard");
        }


    }
}
