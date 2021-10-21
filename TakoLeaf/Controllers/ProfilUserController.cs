using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Controllers
{
    public class ProfilUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profil(int id)
        {
            return View();
        }
    }
}
