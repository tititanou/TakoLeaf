using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TakoLeaf.Data;
using TakoLeaf.Models;

namespace TakoLeaf.Controllers
{
    [Authorize]
    public class MessagerieController : Controller
    {
        private IDalMessagerie dalMessagerie;
        private int userId;

        public MessagerieController()
        {
            this.dalMessagerie = new DalMessagerie();

        }

        public ActionResult MessagesEnvoyes()
        {
            userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Message> messages = this.dalMessagerie.GetMessageEnvoyes(userId).ToList();
            return View(messages);
        }

        public ActionResult MessagesRecus()
        {
            userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Message> messages = this.dalMessagerie.GetMessageRecus(userId).ToList();
            return View(messages);
        }

        [HttpPost]
        public ActionResult SupprimerMessages(List<int> MessageList)
        {
            userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string retour = null;
            if (MessageList.Count != 0)
            {
                 for (int i = 0; i < MessageList.Count(); i++)
                {
                    int id = MessageList[i];
                    Message message = this.dalMessagerie.GetMessage(id);
                    if (message.AdherentDestId == userId)
                    {
                        MessageRecu messageRecu = this.dalMessagerie.GetMessageRecu(id);
                        this.dalMessagerie.SuppressionMessageRecu(messageRecu);
                        retour = "MessagesRecus";
                    }
                    else if (message.AdherentExpId == userId)
                    {
                        MessageEnvoye messageEnvoye = this.dalMessagerie.GetMessageEnvoye(id);
                        this.dalMessagerie.SuppressionMessageEnvoye(messageEnvoye);
                        retour = "MessagesEnvoyes";
                    }
                }
            }
            if(retour == null)
            {
                return View("Error");
            }
            else
            {
                return RedirectToAction(retour);
            }
            
            
        }

        [HttpPost]
        public ActionResult Supprimer(int Id)
        {
            userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (Id != 0)
            {
                Message message = this.dalMessagerie.GetMessage(Id);
                if (message.AdherentDestId == userId)
                {
                    MessageRecu messageRecu = this.dalMessagerie.GetMessageRecu(Id);
                    this.dalMessagerie.SuppressionMessageRecu(messageRecu);
                    return RedirectToAction("MessagesRecus");
                }
                else if (message.AdherentExpId == userId)
                {
                    MessageEnvoye messageEnvoye = this.dalMessagerie.GetMessageEnvoye(Id);
                    this.dalMessagerie.SuppressionMessageEnvoye(messageEnvoye);
                    return RedirectToAction("MessagesEnvoyes");
                }
                else
                {
                    return View("Error");
                }
                
            }
            return View("Error");
        }

        public ActionResult Message(int id)
        {
            Message message = this.dalMessagerie.GetMessage(id);
            return View(message);
        }

        public ActionResult NouveauMessage()
        {
            List<Adherent> adherents = this.dalMessagerie.GetAdherents();
            ViewBag.AdhList = new List<Adherent>(adherents);
            return View();
        }

        [HttpPost]
        public ActionResult NouveauMessage(int Destinataire, string Objet, string Message)
        {
            userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Message message = new Message
            {
                AdherentExpId = userId,
                Date = DateTime.Now,
                Lu = false,
                AdherentDestId = Destinataire,
                Titre = Objet,
                Msg = Message
            };
            
            if (ModelState.IsValid)
            {
                this.dalMessagerie.CreationMessage(message);
                return RedirectToAction("MessagesRecus");
            }
            return View(message);
        }
    }
}