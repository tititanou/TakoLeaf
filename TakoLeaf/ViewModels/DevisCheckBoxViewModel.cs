using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class DevisCheckBoxViewModel
    {
        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
        public int RessourceId { get; set; }
        public Ressource Ressource { get; set; }

        public string Intitule { get; set; }
        public bool EstSelectione { get; set; }
        [Display(Name = "Tarif horaire en €")]
        public double TarifHoraire { get; set; }
    }
}
