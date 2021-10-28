using System;
namespace TakoLeaf.Models
{
    public class PostSignale
    {
        public int Id { get; set; }

        public bool Vu { get; set; }
        public string Message { get; set; }

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }

        public int PostId { get; set; }
        public Sujet Post { get; set; }
    }
}
