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

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
    public enum CateRessource
    {
        OUTIL,
        OUTIL_SPECIALISE,
        LOCAL_GARAGE,
        TERRAIN,
        REMORQUE,
        PONT_ELEVATEUR
    }
}
