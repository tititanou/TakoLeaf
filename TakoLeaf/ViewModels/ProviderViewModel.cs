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
        public CompteUser CompteUser { get; set; }

        public Provider Provider { get; set; }

        public Rib Rib { get; set; }

        public List<Ressource> Ressources { get; set; }

        public List<Competence> Competences { get; set; }
        public List<SsCateCompetence> SsCateCompetences { get; set; }

    }
}
