using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Range(0.0, 5)]
        public double Note { get; set; }

        public int IdAdherent { get; set; }
        public Adherent Adherent { get; set; }

        public int IdRib { get; set; }
        public Rib Rib { get; set; }

        public int IdRessource { get; set; }
        public Ressource Ressource { get; set; }
        
        public int IdCompetence { get; set; } 
        public Competence Competence { get; set; }




    }
}
