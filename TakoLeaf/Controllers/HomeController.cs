using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;

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
    }
}
