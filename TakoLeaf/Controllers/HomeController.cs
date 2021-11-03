using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;

namespace TakoLeaf.Controllers
{
    public class HomeController : Controller
    {
        private IDalHome dalhome;

        public HomeController()
        {
            dalhome = new DalHome();
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Actualites()
        {
            using (IDalAdmin dal = new DalAdmin())
            {
                List<Article> liste = dal.ObtenirTousLesArticlesPublic();
                return View(liste);
            }
            
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
