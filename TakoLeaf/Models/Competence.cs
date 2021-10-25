using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Competence
    {
        
        public int Id { get; set; }
        
        [Required]
        public double TarifHoraire { get; set; }
        public string NomSsCate { get; set; }

        public int SsCateCompetenceId { get; set; }
        public SsCateCompetence SsCateCompetence { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }


    }
}
