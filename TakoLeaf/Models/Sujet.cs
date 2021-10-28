using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Sujet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Sujet")]
        [Required(ErrorMessage = "Veuillez indiquer un titre pour votre sujet")]
        [Column(TypeName = "varchar(100)")]
        public string Titre { get; set; }
        public int IdAdherent { get; set; }
        public Adherent Adherent { get; set; }
    }
}
