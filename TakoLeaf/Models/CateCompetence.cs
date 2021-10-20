using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakoLeaf.Models
{
    public class CateCompetence
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Intitule { get; set; }
    }
}
