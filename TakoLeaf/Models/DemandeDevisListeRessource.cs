using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class DemandeDevisListeRessource
    {

        public int Id { get; set; }

        public int RessourceId { get; set; }
        public Ressource Ressource { get; set; }

        public int DemandeDevisId { get; set; }
        public DemandeDevis DemandeDevis { get; set; }
        
    }
}
