using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class DemandeDevis
    {

        public int Id { get; set; }    
        public DateTime DateDebutVoulue { get; set; }
        [Required(ErrorMessage = "N'oubliez pas d'écrire un message à votre interlocuteur !")]
        [MaxLength(150)]
        public string Message { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer {get; set;}
        public int ProviderId { get; set; }
        public Provider Provider {get; set;}
        public int VoitureId { get; set; }
        public Voiture Voiture {get; set;}
        public List<Competence> CompetenceId { get; set; }
        public Competence Competence {get; set;}

        public List<Ressource> RessourceId { get; set; }
        public Ressource Ressource {get; set;}


    }
}
