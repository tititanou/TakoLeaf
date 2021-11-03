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

        public bool CreationMessage(Message message)
        {
            bool creaOk = false;

            try
            {
            this._bddContext.Messages.Add(message);
            this._bddContext.SaveChanges();
            MessageEnvoye messageEnvoye = new MessageEnvoye
            {
                MessageId = message.Id,
                ExpediteurId = message.AdherentExpId,
                DestinataireId = message.AdherentDestId
            };
            MessageRecu messageRecu = new MessageRecu
            {
                MessageId = message.Id,
                ExpediteurId = message.AdherentExpId,
                DestinataireId = message.AdherentDestId
            };
            this._bddContext.MessageEnvoyes.Add(messageEnvoye);
            this._bddContext.MessageRecus.Add(messageRecu);
            this._bddContext.SaveChanges();
                creaOk = true;

            }
            catch (Exception e)
            {
                creaOk = false;
                Console.WriteLine(e.Source);
            }
            return creaOk;
        }

        public List<Message> GetMessageRecus(int id)
        {
            List<MessageRecu> allMessages = this._bddContext.MessageRecus.Include(m => m.Message).ThenInclude(m=>m.AdherentDest).Include(m=>m.Message).ThenInclude(m=>m.AdherentExp).ToList();
            List<Message> messageRecus = new List<Message>();
            foreach(MessageRecu message in allMessages)
            {
                if (message.DestinataireId == id)
                {
                    messageRecus.Add(message.Message);
                }
            }
            return messageRecus;
        }

        public List<Message> GetMessageEnvoyes(int id)
        {
            List<MessageEnvoye> allMessages = this._bddContext.MessageEnvoyes.Include(m => m.Message).ThenInclude(m => m.AdherentDest).Include(m => m.Message).ThenInclude(m=>m.AdherentExp).ToList();
            List<Message> messageEnvoyes = new List<Message>();
            foreach (MessageEnvoye message in allMessages)
            {
                if (message.ExpediteurId == id)
                {
                    messageEnvoyes.Add(message.Message);
                }
            }
            return messageEnvoyes;
        }

        public Message GetMessage(int id)
        {
            List<Message> allMessages = this._bddContext.Messages.Include(m => m.AdherentExp).Include(m => m.AdherentDest).ToList();
            Message message = null;
            foreach(Message messageItem in allMessages)
            {
                if(messageItem.Id == id)
                {
                    message = messageItem;
                }
            }
            return message;
        }

        public MessageRecu GetMessageRecu(int id)
        {
            List<MessageRecu> allMessageRecus = this._bddContext.MessageRecus.ToList();
            MessageRecu messageRecu = null;
            foreach (MessageRecu messageItem in allMessageRecus)
            {
                if (messageItem.Id == id)
                {
                    messageRecu = messageItem;
                }
            }
            return messageRecu;
        }

        public MessageEnvoye GetMessageEnvoye(int id)
        {
            List<MessageEnvoye> allMessageEnvoyes = this._bddContext.MessageEnvoyes.ToList();
            MessageEnvoye message = null;
            foreach (MessageEnvoye messageItem in allMessageEnvoyes)
            {
                if (messageItem.Id == id)
                {
                    message = messageItem;
                }
            }
            return message;
        }

        public void SuppressionMessageRecu(MessageRecu messageRecu)
        {
            Message message = this.GetMessage(messageRecu.Id);
            if(this.GetMessageEnvoye(messageRecu.Id) == null)
            {
                this._bddContext.Messages.Remove(message);
            }
            this._bddContext.MessageRecus.Remove(messageRecu);
            this._bddContext.SaveChanges();
        }

        public void SuppressionMessageEnvoye(MessageEnvoye messageEnvoye)
        {
            MessageRecu messageRecu = this.GetMessageRecu(messageEnvoye.Id);
            Message message = this.GetMessage(messageEnvoye.Id);
            if (messageRecu == null)
            {
                this._bddContext.Messages.Remove(message);
            }
            this._bddContext.MessageEnvoyes.Remove(messageEnvoye);
            this._bddContext.SaveChanges();
        }

        public List<Adherent> GetAdherents()
        {
            return this._bddContext.Adherents.ToList();
        }
    }
}
