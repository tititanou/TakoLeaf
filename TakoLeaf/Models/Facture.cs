using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Facture
    {
        [Key]
        [MaxLength(8)]
        public string NumeroFacture { get; set; }

        public DateTime DateEmission { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer {get; set;}



    }
}
