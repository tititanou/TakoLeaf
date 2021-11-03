using System;
using System.ComponentModel.DataAnnotations;

namespace TakoLeaf.Models
{
    public class Ressource
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public CateRessource Categorie { get; set; }
        public int AdresseId { get; set; }
        public Adresse Adresse { get; set; }
        public double TarifJournalier { get; set; }
        public bool Disponible{ get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
    public enum CateRessource
    {
        [Display(Name="Outil")]
        OUTIL,
        [Display(Name = "Outil Spécialisé")]
        OUTIL_SPECIALISE,
        [Display(Name = "Local/Garage")]
        LOCAL_GARAGE,
        [Display(Name = "Terrain")]
        TERRAIN,
        [Display(Name = "Remorque")]
        REMORQUE,
        [Display(Name = "Pont élévateur")]
        PONT_ELEVATEUR
    }
}
