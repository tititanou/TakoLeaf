using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakoLeaf.Models
{
    public class Admin
    {
        [Key]
        [Required]
        [Display(Name = "Identifiant")]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name ="Mot de passe")]
        public string Pwd { get; set; }
    }
}
