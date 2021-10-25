using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Article
    {

        public int Id { get; set; }
        public string AdminId { get; set; }
        public Admin Admin { get; set; }
        [Required(ErrorMessage = "Merci d'indiquer un titre")]
        [StringLength(100)]
        public string Titre { get; set; }
        public DateTime DateRedaction { get; set; }
        public DateTime DatePublication { get; set; }
        [Required(ErrorMessage = "Attention votre article est vide")]
        public string Texte { get; set; }
        public byte[] Image { get; set; }
        public bool Public { get; set; }






    }
}
