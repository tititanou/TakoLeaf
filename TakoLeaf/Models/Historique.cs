using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TakoLeaf.Models
{
    public class Historique
    {
        
        public int Id{ get; set; }

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }

        //public List<Prestation> Prestations { get; set; }
    }
}
