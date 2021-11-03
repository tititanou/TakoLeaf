using System;
using System.Collections.Generic;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public interface IDalMessagerie : IDisposable
    {
        public bool CreationMessage(Message message);

        public List<Message> GetMessageRecus(int id);

        public List<Message> GetMessageEnvoyes(int id);

        public Message GetMessage(int id);

        MessageRecu GetMessageRecu(int id);

        MessageEnvoye GetMessageEnvoye(int id);

        void SuppressionMessageRecu(MessageRecu message);

        void SuppressionMessageEnvoye(MessageEnvoye message);

        List<Adherent> GetAdherents();
    }
}
