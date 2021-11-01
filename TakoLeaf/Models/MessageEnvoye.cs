using System;
namespace TakoLeaf.Models
{
    public class MessageEnvoye
    {
        public int Id { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }

        public int ExpediteurId { get; set; }
        public Adherent Expediteur { get; set; }

        public int DestinataireId { get; set; }
        public Adherent Destinataire { get; set; }

    }
}
