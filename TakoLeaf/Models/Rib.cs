using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Rib
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du titulaire est obligatoire")]
        [Column(TypeName = "varchar(40)")]
        public string Titulaire { get; set; }

        [Required(ErrorMessage = "L'IBAN est obligatoire")]
        [StringLength(27, ErrorMessage = "L'IBAN doit comporter 27 caractères")]
        [Column(TypeName = "varchar(27)")]
        public string Iban { get; set;}

        [Required(ErrorMessage = "Le nom de la banque est obligatoire")]
        [Column(TypeName = "varchar(40)")]
        public string Banque { get; set; }


    }
}
