using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class DevisViewModel
    {

        //public Adherent Adherent { get; set; }
        public Consumer Consumer { get; set; }
        public Provider Provider { get; set; }
        public int IdC { get; set; }
        public int IdP { get; set; }
        public double Tarif { get; set; }
       
        public List<DemandeDevisViewModel> ListeDemandeDevis { get; set; }
        public DemandeDevis DemandeDevis { get; set; }
        public List<DemandeDevis> DemandesDevis { get; set; }
        public List<DemandeDevisListeCompetence> ListeCompetencesDevis { get; set; }
        public List<DemandeDevisListeRessource> ListeRessourcesDevis { get; set; }
        public List<Ressource> Ressources { get; set; }
        public List<Competence> Competences { get; set; }
        public Devis Devis { get; set; }

        public List<DevisCheckBoxViewModel> ListD { get; set; }
        public List<DevisCheckBoxViewModel> ListR { get; set; }
        public Voiture Voiture { get; set; }
    }
}
