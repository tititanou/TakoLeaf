using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Consumer
    {
       
        public int Id { get; set; }
       

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }
       
        

    }
}
