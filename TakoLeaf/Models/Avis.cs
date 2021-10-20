using System;
namespace TakoLeaf.Models
{
    public class Avis
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public double Note { get; set; }
        public string Contenu { get; set; }

        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
