using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Modele
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Display(Name = "Nom Modele")]
        public string Nom { get; set; }

        public int MarqueId { get; set; }
        public Marque Marque { get; set; }
    }
}
