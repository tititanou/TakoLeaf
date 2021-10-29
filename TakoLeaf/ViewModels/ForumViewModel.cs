using System;
using System.Collections.Generic;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class ForumViewModel
    {
        public Sujet Sujet { get; set; }
        public List<Post> Posts { get; set; }
        public Adherent Adherent { get; set; }
        public Post Post { get; set; }
        public PostSignale PostSignale { get; set; }
    }
}