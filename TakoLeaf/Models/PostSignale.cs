using System;
namespace TakoLeaf.Models
{
    public class PostSignale
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public bool Vu { get; set; }
        public string Message { get; set; }

        public int AdherentSignaleId { get; set; }
        public Adherent AdherentSignale { get; set; }

        public int AdherentSignalantId { get; set; }
        public Adherent AdherentSignalant { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
