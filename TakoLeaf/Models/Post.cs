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

        [Required(ErrorMessage = "Le message est obligatoire")]
        public string Corps_Post { get; set; }

        public int IdAdherent { get; set; }
        public Adherent Adherent {get ; set;}

    }
}
