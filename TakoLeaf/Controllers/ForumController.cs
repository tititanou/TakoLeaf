using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class ForumController : Controller
    {
        private IDalForum dalForum;
        public ForumController()
        {
            this.dalForum = new DalForum();
        }

        public IActionResult Sujets()
        {
            List<Sujet> sujets = dalForum.GetAllSujets().ToList();
            return View(sujets);
        }

        public IActionResult Sujet(int id)
        {
            ForumViewModel fvm = null;
            if (id != null)
            {
                fvm = new ForumViewModel() {
                    Sujet = dalForum.GetSujet(id),
                    Posts = dalForum.GetPosts(id) };
                return View(fvm);
            }
            return View("error");
        }

        public IActionResult Repondre(int id)
        {
            return View();
        }

        public IActionResult NouveauSujet()
        {
            return View();
        }

    }
}