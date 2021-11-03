using System;
using System.Collections.Generic;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class MessagerieViewModel
    {
        public Message Message { get; set; }
        public Adherent Adherent { get; set; }
        public List<Message> MessagesList { get; set; }
    }
}
