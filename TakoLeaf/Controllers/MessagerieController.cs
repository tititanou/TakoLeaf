using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}