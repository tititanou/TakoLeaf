using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private IDalForum dalForum;
        private int userId;
        public ForumController()
        {
            this.dalForum = new DalForum();
            Console.WriteLine(userId);
        }

        public ActionResult Sujets()
        {
            List<Sujet> sujets = dalForum.GetAllSujets().ToList();
            return View(sujets);
        }

        public ActionResult Sujet(int? id)
        {
            ForumViewModel fvm = null;
            if (!id.HasValue)
            {
                return View("Error");
            }
            fvm = new ForumViewModel()
            {
                Sujet = dalForum.GetSujet(id.Value),
                Posts = dalForum.GetPosts(id.Value)
            };
            return View(fvm);
        }

        public ActionResult ModifierPost(int? id)
        {
            Post post = null;
            if(id.HasValue)
            {
                post = dalForum.Get1Post(id.Value);
                dalForum.ModificationPost(post);
                return View(post);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult ModifierPost(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);
            this.dalForum.ModificationPost(post);
            return RedirectToAction("Sujet", post.SujetId);
        }

        public ActionResult ModifierSujet(int? id)
        {
            Sujet sujet = null;
            if(id.HasValue)
            {
                sujet = dalForum.GetSujet(id.Value);
                dalForum.ModificationSujet(sujet);
                return View();
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult ModifierSujet(Sujet sujet)
        {
            if (!ModelState.IsValid)
                return View(sujet);
            this.dalForum.ModificationSujet(sujet);
            return RedirectToAction("Sujets");
        }

        public ActionResult SupprimerSujet(int? id)
        {
            Sujet sujet = null;
            if(id.HasValue)
            {
                sujet = dalForum.GetSujet(id.Value);
                dalForum.SuppressionSujet(sujet);
                return View("Sujets");
            }
            return View("Error");
        }

        public ActionResult SupprimerPost(int? id)
        {
            Post post = null;
            if(id.HasValue)
            {
                post = dalForum.Get1Post(id.Value);
                int idSujet = post.SujetId;
                dalForum.SuppressionPost(post);
                return View("Sujet", idSujet);
            }
            return View("Error");
        }

        public ActionResult Repondre(int id)
        {
            return View();
        }

        public ActionResult Citer(int id)
        {
            return View();
        }

        public ActionResult NouveauSujet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NouveauSujet(string Titre, string CorpPost) {
            BddContext _bddContext = new BddContext();
            DateTime now = DateTime.Now;
            Sujet sujet = new Sujet() { Date = now, Titre = Titre };
            this.dalForum.CreationSujet(sujet);
            Sujet s = this.dalForum.RechercheSujetParTitre(Titre);
            Post post = new Post() { AdherentId = this.userId, CorpsPost = CorpPost, Date = now, SujetId = s.Id };
            ForumViewModel fvm = new ForumViewModel()
            {
                Sujet = sujet,
                Post = post,
                //Adherent = _bddContext.Adherents.Find(this.userId)
            };

            return View(fvm);
        }

        public ActionResult UPosts()
        {
            return View();
        }
    }
}