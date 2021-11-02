using System;
using System.ComponentModel.DataAnnotations;

namespace TakoLeaf.Models
{
    public class Avis
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        [Range(0.0,5.0, ErrorMessage = "La note doit se situer entre 0 et 5")]
        public double Note { get; set; }
        public string Contenu { get; set; }

        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int PrestationId { get; set; }
        public Prestation Prestation { get; set; }
    }
}
