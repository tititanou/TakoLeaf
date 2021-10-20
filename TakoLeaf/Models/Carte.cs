using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Carte
    {
        public int Id { get; set; }
        public string Titulaire { get; set; }
        public float NumeroCarte { get; set; }
        public string ExpirDate { get; set; }
        public int Crypto { get; set; }
    }
}
