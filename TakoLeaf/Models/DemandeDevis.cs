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
        public int IdConsumer { get; set; }
        public Consumer Consumer {get; set;}
        public int IdProvider { get; set; }
        public Provider Provider {get; set;}
        public int IdVoiture { get; set; }
        public Voiture Voiture {get; set;}
        public List<int> IdCompetence { get; set; }
        public List<int> Competence Competences {get; set;}

        public List<int> IdRessource { get; set; }
        public List<int> Ressource Ressources {get; set;}


    }
}
