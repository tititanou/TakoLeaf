using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Carte
    {

        
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Titulaire { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Le nombre de la carte doit comporter 16 caractères")]
        [RegularExpression("^4[0-9]{12}$", ErrorMessage = "Le numero de carte n'est pas valide")]
        public string NumeroCarte { get; set; }

        [Required(ErrorMessage = "La Date est obligatoire")]
        //[RegularExpression("(0[1-9]|10|11|12)/20[0-9]{2}$", ErrorMessage = "La Date n'est pas valide")]
        public string ExpirDate { get; set; }

        [Required(ErrorMessage = "Le cryptogramme est obligatoire")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Le Cryptogramme est de 3 chiffres")]
        public int Crypto { get; set; }

        public int ConsumerId { get; set;}
        public Consumer Consumer { get; set; }
    }
}
