using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TakoLeaf.Models
{
    public class Historique
    {
        [Key]
        public int IdAdherent { get; set; }
        public Adherent Adherent { get; set; }

        public List<Prestation> Prestations { get; set; }
    }
}
