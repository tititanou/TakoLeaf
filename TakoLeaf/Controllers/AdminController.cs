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
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }

            DashViewModel dash = new DashViewModel();

            List<Prestation> presta = dal.ObtenirToutesLesPrestations();
            presta.OrderBy(p => p.DateDebut);
            List<Prestation> presta2 = presta;

            List<CompteUser> adherents = dal.ObtenirAdherentsEtComptes();
            adherents.OrderBy(a => a.Adherent.DateInscription);


            dash.ListePrestations = presta2;
            dash.ListeAdherents = dal.ObtenirTousLesAdherents();
            dash.ListeCompte = adherents;

            return View(dash);
        }

        // Pages relatives à la gestion de compte

        public ActionResult GestionComptes()
        {

            //User.FindFirst(ClaimTypes.NameIdentifier).Value


            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            List<CompteUser> liste = dal.ObtenirTousLesAdherentsEtComptes();
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
        public ActionResult InvaliderProfil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            dal.ChangerEtatProfil(id, 3);
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


        public ActionResult AfficherPrestation(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }

            Prestation p = dal.ObtenirPrestation(id);

            PrestationViewModel prestation = new PrestationViewModel
            {

                Presta = p,
                CompteUserConsumer = dal.ObtenirCompteUser(p.Consumer.AdherentId),
                CompteUserProvider = dal.ObtenirCompteUser(p.Provider.AdherentId),
                ProviderRib = dal.ObtenirRib(p.Provider.AdherentId)

            };

            return View(prestation);
        }


        //public ActionResult ValiderPrestation(int idPrestation)
        //{

        //}

        public ActionResult AfficherProfil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            AdherentViewModel Adherent = new AdherentViewModel
            {
                Compte = dal.ObtenirAdherentEtCompte(id),
                PieceJusti = dal.ObtenirPieceJustificative(id),
                InfoProvider = dal.ObtenirProvider(id)
            }
            ;
            return View(Adherent);

        }

        public ActionResult AfficherPostsSignales()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }
            List<PostSignale> liste = dal.ObtenirLesPostesSignales();
            return View(liste);
        }

        public ActionResult AfficherTransactions()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Home/Index");
            }

            DashViewModel dash = new DashViewModel();
            dash.ListePrestations = dal.ObtenirToutesLesPrestations();
            dash.ListeAdherents = dal.ObtenirTousLesAdherents();
            dash.ListeCompte = dal.ObtenirAdherentsEtComptes();

            return View(dash);
        }

        public ActionResult ValiderTransaction(int id)
        {
            dal.ValiderTransaction(id);
            return Redirect("/Admin/AfficherTransactions");
        } 
    }
}
