using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Marque
    { 
        public int Id { get; set; }
        [MaxLength(30)]
        [Display(Name = "Nom Marque")]
        public string Nom { get; set; }

    }
}
