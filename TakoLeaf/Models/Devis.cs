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
        [Column(TypeName = "varchar(50)")]
        public string NumeroDevis { get; set; }
        public DateTime DateEmission { get; set; }
        [Required]
        public double Tarif { get; set; }
        //public byte[] Fichier { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin { get; set; }
        [Display(Name = "Description de la prestation")]
        public string DescriptionPresta { get; set; }

        [Required]
        public int LieuPrestaId { get; set; }
        public Adresse LieuPresta { get; set; }
        public EtatDevis EtatDevis { get; set; }

        
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        public int VoitureId { get; set; }
        public Voiture Voiture { get; set; }

        //public List<Competence> CompetenceL { get; set; }
        //public Competence Competence { get; set; }

        //public List<Ressource> RessourceL { get; set; }
        //public Ressource Ressource { get; set; }

        public int DemandeDevisId { get; set; }
        public DemandeDevis DemandeDevis { get; set; }

    }
    public enum EtatDevis
    {
        ACCEPTE,
        EN_ATTENTE,
        REFUSE
    }
}
