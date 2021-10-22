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

        // Pages relatives à la gestion de compte

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
        public ActionResult SupprimerCompte(int id)
        {
            dal.SupprimerProfil(id);
            return RedirectToAction("Dashboard");
        }

        // Pages relatives à la publication d'article

        public ActionResult PublicationArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublicationArticle(Article article)
        {
            dal.AjouterArticle(article.Titre, article.Texte);
            return View();
        }

        public ActionResult Articles()
        {
            List<Article> liste = dal.ObtenirTousLesArticles();
            return View(liste);
        }

        public ActionResult ModifierArticle(int id)
        {
            Article article = dal.ObtenirArticle(id);

            return View(article);
        }
        [HttpPost]
        public ActionResult ModifierArticle(Article article)
        {
            dal.ModifierArticle(article.Id, article.Titre, article.Texte);

            return RedirectToAction("Articles");
        }

        public ActionResult SuppressionArticle(int id)
        {
            dal.SupprimerArticle(id);

            return RedirectToAction("Articles");

        }



    }
}
