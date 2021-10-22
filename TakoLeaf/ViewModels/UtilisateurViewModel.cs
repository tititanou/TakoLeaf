using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class UtilisateurViewModel
    {
        public Adherent Adherent { get; set; }
        public CompteUser CompteUser { get; set; }
        public Consumer Consumer { get; set; }
        public Provider Provider { get; set; }
        public Voiture Voiture { get; set; }
        public Carte Carte { get; set; }
        public Marque Marque { get; set; }
        public Modele Modele { get; set; }
        
    }
}
