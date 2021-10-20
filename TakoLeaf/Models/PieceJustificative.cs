using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class PieceJustificative
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public byte[] Fichier { get; set; }

        [Required]
        public string Description { get; set; } 

        public int AdherentId { get; set; }

        public Adherent Adherant { get; set; }


    }
}
