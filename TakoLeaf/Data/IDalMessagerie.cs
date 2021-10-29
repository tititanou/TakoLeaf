using System;
using System.Collections.Generic;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public interface IDalMessagerie : IDisposable
    {
        public void CreationMessage(Message message);

        public List<Message> GetMessageRecus(int id);

        public List<Message> GetMessageEnvoyes(int id);

        public Message GetMessage(int id);

        public void SuppressionMessage(Message message);
    }
}
