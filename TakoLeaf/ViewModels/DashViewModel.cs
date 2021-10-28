using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class DashViewModel
    {
        public List<Prestation> ListePrestations { get; set; }
        public List<Adherent> ListeAdherents { get; set; }
        public List<CompteUser> ListeCompte { get; set; }
    }
}
