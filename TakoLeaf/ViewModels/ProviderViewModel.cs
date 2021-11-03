using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class ProviderViewModel
    {
        public Adherent Adherent { get; set; }

        public Provider Provider { get; set; }

        public CompteUser CompteUser { get; set; }

        public List<Competence> Competence { get; set; }
        public CateCompetence CateCompetence { get; set; }
        public SsCateCompetence SsCateCompetence { get; set; }
        public List<ProviderCheckBoxViewModel> ListSSC { get; set; }
        public Rib Rib { get; set; }
        public List<Ressource> Ressources { get; set; }
        public Ressource Ressource { get; set; }
        public bool Amis { get; set; }
        public List<Avis> Avis { get; set; }
    }
}
