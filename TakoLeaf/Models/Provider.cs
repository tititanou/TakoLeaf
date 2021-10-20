﻿using System;
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

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }

        public int RibId { get; set; }
        public Rib Rib { get; set; }

        public List<Ressource> RessourceId { get; set; }
        public Ressource Ressource { get; set; }
        
        public List<Competence> CompetenceId { get; set; } 
        public Competence Competence { get; set; }




    }
}
