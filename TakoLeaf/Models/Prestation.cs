using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Prestation
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateVoulu { get; set; }

        [Required]
        public int prix { get; set; }

        
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        
        public int VoitureId { get; set; }
        public Voiture Voiture { get; set; }

        
        public int Numero_Devis { get; set; }
        public Devis Devis { get; set; }

        
        public Etat etatPresta { get; set; }
        public enum Etat
        {
            Refuse,
            En_cours,
            Valide
        }

    }
}
