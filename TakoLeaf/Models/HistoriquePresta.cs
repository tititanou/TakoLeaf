using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class HistoriquePresta
    {

        public int Id { get; set; }
        public int HistoriqueId { get; set; }
        public Historique Historique { get; set; }

        public int PrestationId { get; set; }
        public Prestation Prestation { get; set; }




    }
}
