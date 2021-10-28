using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Amitie
    {

        public int Id { get; set; }
        public int AdherantId_Courant { get; set; }
        public int AdherantId_Ami { get; set; }

        public Adherent Adherent { get; set; }
    }
}
