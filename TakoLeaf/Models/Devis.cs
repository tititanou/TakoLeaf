using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Devis
    {
        [Key]        
        public string NumeroDevis { get; set; }
        public DateTime DateEmission { get; set; }
        [Required]
        public int Tarif { get; set; }
        public byte[] Fichier { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin { get; set; }
        public string DescriptionPresta { get; set; }

        [Required]
        public string LieuPresta { get; set; }
        public EtatDevis EtatDevis { get; set; }

        
        public int IdProvider { get; set; }
        public Provider Provider { get; set; }

        public int IdConsumer { get; set; }
        public Consumer Consumer { get; set; }

        public int IdVoiture { get; set; }
        public Voiture Voiture { get; set; }

        public List<int> IdCompetence { get; set; }
        public List<Competence> Competence { get; set; }

        public List<int> IdRessource { get; set; }
        public List<Ressource> Ressource { get; set; }

        public int IdDemande { get; set; }
        public DemandeDevis DemandeDevis { get; set; }

    }
    public enum EtatDevis
    {
        ACCEPTE,
        EN_ATTENTE,
        REFUSE
    }
}
