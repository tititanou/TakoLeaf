using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalMessagerie : IDalMessagerie
    {
        private BddContext _bddContext;

        public DalMessagerie()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public void CreationMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessageRecus(int id)
        {
            List<Message> allMessages = this._bddContext.Messages.Include(m => m.AdherentDest).Include(m => m.AdherentExp).ToList();
            List<Message> messageRecus = new List<Message>();
            foreach(Message message in allMessages)
            {
                if (message.AdherentDestId == id)
                {
                    messageRecus.Add(message);
                }
            }
            return messageRecus;
        }

        public List<Message> GetMessageEnvoyes(int id)
        {
            List<Message> allMessages = this._bddContext.Messages.Include(m => m.AdherentDest).Include(m => m.AdherentExp).ToList();
            List<Message> messageEnvoyes = new List<Message>();
            foreach (Message message in allMessages)
            {
                if (message.AdherentExpId == id)
                {
                    messageEnvoyes.Add(message);
                }
            }
            return messageEnvoyes;
        }

        public Message GetMessage(int id)
        {
            throw new NotImplementedException();
        }

        public void SuppressionMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
