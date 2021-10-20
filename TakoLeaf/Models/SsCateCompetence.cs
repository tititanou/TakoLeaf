using System;
namespace TakoLeaf.Models
{
    public class SsCateCompetence
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public int CateCompetenceId { get; set; }
        public CateCompetence CateCompetence { get; set; }
    }
}
