using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                return Redirect("/Home/Index");
            }
            return View(viewModel);

        }

        public ActionResult Dashboard()
        {
            return View();
        }

        // Pages relatives à la gestion de compte

        public ActionResult GestionComptes()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            List<UtilisateurViewModel> liste = dal.ObtenirTousLesAdherentsEtComptes();
            return View(liste);
        }

        public ActionResult ValiderProfil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.ChangerEtatProfil(id, 0);
            return RedirectToAction("GestionComptes");
        }
        public ActionResult BloquerProfil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.ChangerEtatProfil(id, 1);
            return RedirectToAction("GestionComptes");
        }
        public ActionResult DebloquerProfil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.ChangerEtatProfil(id, 2);
            return RedirectToAction("GestionComptes");
        }
        public ActionResult SupprimerCompte(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.SupprimerProfil(id);
            return RedirectToAction("GestionComptes");
        }

        // Pages relatives à la publication d'article

        public ActionResult PublicationArticle()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PublicationArticle(Article article)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.PublierArticle(article);
            return RedirectToAction();
        }

        [HttpPost]
        public ActionResult StockerArticle(Article article)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.AjouterArticle(article.Titre, article.Texte, false);
            return RedirectToAction("Admin", "GestionActualites");
        }

        public ActionResult ModifierArticle(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            Article article = dal.ObtenirArticle(id);

            return View(article);
        }
        [HttpPost]
        public ActionResult ModifierArticle(Article article)
        {
            dal.ModifierArticle(article.Id, article.Titre, article.Texte);
            ViewBag.Message = "L'article a été mis à jour";

            return Redirect("/Home/Actualites");
        }

        public ActionResult SuppressionArticle(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.SupprimerArticle(id);

            return RedirectToAction("Articles");

        }

        public ActionResult GestionArticles()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            List<Article> list = dal.ObtenirTousLesArticles();
            return View(list);
        }

        public ActionResult ModificationVisibilite(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.ModifierVisibiliteArticle(id);
            return RedirectToAction("GestionArticles");
        }


    }
}
