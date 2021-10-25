using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class ProviderCheckBoxViewModel
    {
        public int SsCateCompetenceId { get; set; }
        public SsCateCompetence SsCateCompetence { get; set; }
        public string Intitule { get; set; }
        public bool EstSelectione { get; set; }
        [Display (Name ="Tarif horaire en €")]
        public int TarifHoraire { get; set; }

    }
}
