using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Voiture
    {
        public int Id { get; set; }
        [Required]
        //[RegularExpression(@"^[A-Z]{2}[-][0-9]{3}[-][A-Z]{2}$)", ErrorMessage = "L'immatriculation doit suivre la forme suivante AA-123-AA")]
        public string Immatriculation { get; set; }
        [Required]
        [MaxLength(40)]
        public string Titulaire { get; set; }
        [Required]
        public Carburant Carburant { get; set; }
        [Required]
        [RegularExpression(@"\d{4}$")] //Une année à 4 chiffres
        public int Annee { get; set; } //string ou int ?
        [Required]
        public int ModeleId { get; set; }
        public Modele Modele { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

    }

    public enum Carburant
    {
       DIESEL,
       SP95,
       SP98,
       ETHANOL,
       GPL,
       HYDROGENE,
       LNG,
       CNG,
       ELECTRIQUE

    }

}
