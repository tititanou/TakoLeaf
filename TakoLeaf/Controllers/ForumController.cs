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
        public ForumController()
        {
            Console.WriteLine("ForumController instancié");
        }

        public IActionResult Sujets()
        {
            return View();
        }

        public IActionResult Sujet(int id)
        {
            ForumViewModel fvm = null;
            if (id != null)
            {
                using(IDalForum dalForum = new DalForum())
                {
                    fvm = new ForumViewModel() { Sujet = dalForum.GetSujet(id), Posts = dalForum.GetPosts(id) };
                    return View(fvm);
                }
            }
            return View("error");
        }
    }
}