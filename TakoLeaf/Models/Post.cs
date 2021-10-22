using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Post
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [Display(Name = "Message")]
        [Required(ErrorMessage = "Le message est obligatoire")]
        public string CorpsPost { get; set; }

        public int AdherentId { get; set; }
        public Adherent Adherent {get ; set;}

        public int SujetId { get; set; }
        public Sujet Sujet { get; set; }

    }
}
