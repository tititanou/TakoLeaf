using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class DemandeDevisListeCompetence
    {
        public int Id { get; set; }
        
        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
        
        public int DemandeDevisId { get; set; }
        public DemandeDevis DemandeDevis { get; set; }
    }
}
