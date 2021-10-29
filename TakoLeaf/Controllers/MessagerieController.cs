using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TakoLeaf.Controllers
{
    [Authorize]
    public class MessagerieController : Controller
    {
        //private IDalMessagerie dalForum;
        private int userId;
        public MessagerieController()
        {
            //this.dalForum = new DalMessagerie();

        }
    }
}