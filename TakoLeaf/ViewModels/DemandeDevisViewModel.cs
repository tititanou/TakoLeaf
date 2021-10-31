using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class DemandeDevisViewModel
    {

        public DemandeDevis DemandeDevis { get; set; }

        public List<DemandeDevisListeCompetence> ListeCompetences { get; set; }
        public List<DemandeDevisListeRessource> ListeRessources { get; set; }
        public Consumer Consumer { get; set; }
        public Voiture Voiture { get; set; }
    }
}
