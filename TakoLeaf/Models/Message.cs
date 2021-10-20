using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakoLeaf.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Msg { get; set; }
        public DateTime Date { get; set; }
        public string Titre { get; set; }
        public bool Lu { get; set; }

        [Display(Name = "Expéditeur")]
        [Column(name: "Expediteur")]
        public int AdherentExpId { get; set; }
        public Adherent AdherentExp { get; set; }

        [Required]
        [Display(Name = "Destinataire")]
        [Column(name:"Destinataire")]
        public int AdherentDestId { get; set; }
        public Adherent AdherentDest { get; set; }
    }
}
