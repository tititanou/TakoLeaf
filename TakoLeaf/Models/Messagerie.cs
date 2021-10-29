using System;
namespace TakoLeaf.Models
{
    public class Messagerie
    {
        
        public int IdMessage { get; set; }
        public Message Message { get; set; }

        public int IdExpediteur { get; set; }
        public Adherent Expediteur { get; set; }

        public int IdDestinataire { get; set; }
        public Adherent Destinataire { get; set; }
        
    }
}
