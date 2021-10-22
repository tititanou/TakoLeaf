using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TakoLeaf.Controllers
{
    public class RechercheController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}