using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Adherent
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [MaxLength(20)] // Permet de passer un string en C# en VARCHAR(20) dans la BDD on peut aussi l'écrire [Column(TypeName = "varchar(200)")]
        public string Nom { get; set; }

        [Display(Name = "Prénom")]
        [Column(TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [MaxLength(20)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Cette information est obligatoire")]
        public DateTime Date_naissance { get; set; }

        [Required(ErrorMessage = "Cette information est obligatoire")]
        [Column(TypeName = "varchar(60)")]
        public string Adresse { get; set; }

        [Display(Name = "Téléphone")]
        [Column(TypeName = "varchar(12)")]
        [Required(ErrorMessage = "Cette information est obligatoire")]
        [RegularExpression(@"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$", ErrorMessage = "Le téléphone doit avoir la forme suivante 0X XX XX XX XX)]")]
        public string Telephone { get; set; }

        [Display(Name = "Consumer")]
        public bool IsConsumer { get; set; }

        [Display(Name = "Provider")]
        public bool IsProvider { get; set; }
    }
}
