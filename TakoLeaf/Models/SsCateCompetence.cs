using System;
namespace TakoLeaf.Models
{
    public class SsCateCompetence
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public int IdCateCompetence { get; set; }
        public CateRessource CateRessource { get; set; }
    }
}
