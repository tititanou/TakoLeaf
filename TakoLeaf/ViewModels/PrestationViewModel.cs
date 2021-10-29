using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class PrestationViewModel
    {
        public Prestation Presta { get; set; }

        public CompteUser CompteUserConsumer { get; set; }

        public CompteUser CompteUserProvider { get; set; }

        public Provider ProviderRib { get; set; }
    }
}
