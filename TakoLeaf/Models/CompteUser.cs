using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        public string MotDePasse { get; set; }
        
        // TODO a voir comment autoriser de ne pas uploader des photos
        public string Avatar {get; set;}
        [Column(TypeName = "longtext")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        public string Description { get; set; }
        public string Amis { get; set; }
        public string UserBloques { get; set; }
        public EtatProfil EtatProfil { get; set; }
        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }
        public string Role { get; set; }


    }

    public enum EtatProfil
    {

        VALIDE,
        NON_VALIDE,
        ATTENTE_VALIDATION,
        COMPTE_BLOQUE

    }

   

}
