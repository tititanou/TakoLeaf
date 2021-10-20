using System;
using System.ComponentModel.DataAnnotations;
namespace TakoLeaf.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Pwd { get; set; }
    }
}
