using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Adresse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [Column(TypeName = "varchar(60)")]
        public string Rue { get; set; }
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [Display(Name = "Code Postal")]
        public int CodePostal { get; set; }
        public int Departement { get; set; }
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [Column(TypeName = "varchar(60)")]
        public string Ville { get; set; }
       
    }
}
