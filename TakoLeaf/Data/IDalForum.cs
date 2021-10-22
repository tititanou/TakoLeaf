using System;
using System.Collections.Generic;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public interface IDalForum : IDisposable
    {
        void CreationSujet(Sujet sujet);
        void CreationPost(Post post);
        List<Post> GetPosts(int id);
        Sujet GetSujet(int id);
        List<Sujet> GetAllSujets();
    }
}
