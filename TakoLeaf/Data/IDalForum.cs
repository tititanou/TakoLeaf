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
        void ModificationSujet(Sujet sujet);
        void ModificationPost(Post post);
        void SuppressionSujet(Sujet sujet);
        void SuppressionPost(Post post);
        Post Get1Post(int id);
        Sujet RechercheSujetParTitre(string titre);
    }
}
