using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Carte
    {

        
        public int Id { get; set; }

        [Required]
        public string Titulaire { get; set; }

        [Required]
        public float NumeroCarte { get; set; }

        [Required]
        public string ExpirDate { get; set; }

        [Required]
        public int Crypto { get; set; }
    }
}
