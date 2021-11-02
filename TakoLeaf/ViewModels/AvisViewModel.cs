using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class AvisViewModel
    {
        public Prestation Prestation { get; set; }
        public Avis Avis { get; set; }
        public Provider Provider { get; set; }

        public List<HistoriquePresta> HistoriquePrestas { get; set; }

    }
}
