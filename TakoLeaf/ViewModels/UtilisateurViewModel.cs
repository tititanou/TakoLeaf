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
        public List<Provider> Providers { get; set; }
        public Voiture Voiture { get; set; }
        public Adresse Adresse { get; set; }
        public List<Voiture> Voitures { get; set; }
        public List<Carte> Cartes { get; set; }
        public Carte Carte { get; set; }
        public Marque Marque { get; set; }
        public Modele Modele { get; set; }
        public List<string> Modeles { get; set; }
        public List<string> Marques { get; set; }
        public List<Amitie> Amities { get; set; }
        public bool Amis { get; set; }
        public List<Avis> Avis { get; set; }


        
    }
}
