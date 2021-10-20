using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class CompteUser
    {
        [Key]
        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        public string Mail { get; set; }
        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        public string MotDePasse { get; set; }
        public byte Avatar {get; set;}
        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        public string Description { get; set; }
        public EtatProfil EtatDuProfil { get; set; }
        public List<String> Amis { get; set; }
        public List<String> UserBloques { get; set; }
        public EtatProfil EtatProfil { get; set; }
        public int IdAdherent { get; set; }
        public Adherant Adherant { get; set; }

    }

    public enum EtatProfil
    {

        VALIDE,
        NON_VALIDE,
        ATTENTE_VALIDATION,

    }

}
