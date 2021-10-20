using System;
namespace TakoLeaf.Models
{
    public class Ressource
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public CateRessource Categorie { get; set; }
        public string Adresse { get; set; }
        public double TarifJournalier { get; set; }
        public bool Disponible{ get; set; }
    }
}
