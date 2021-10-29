using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Amitie
    {

        public int Id { get; set; }
        
        public int AdherentCourantId { get; set; }
        
        public int AdherentAmiId { get; set; }

        public Adherent AdherentCourant { get; set; }
        public Adherent AdherentAmi { get; set; }
    }
}
