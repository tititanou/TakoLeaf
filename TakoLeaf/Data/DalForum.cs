using System;
using System.Collections.Generic;
using System.Linq;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalForum : IDalForum
    {
        private BddContext _bddContext;

        public DalForum()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public void CreationSujet(Sujet sujet)
        {
            this._bddContext.Sujets.Add(sujet);
            this._bddContext.SaveChanges();
        }

        public void CreationPost(Post post)
        {
            this._bddContext.Posts.Add(post);
            this._bddContext.SaveChanges();
        }

        public Sujet GetSujet(int id)
        {
            Sujet sujet = null;
            List<Sujet> sujets = this._bddContext.Sujets.ToList();
            for(int i = 0; i < sujets.Count(); i++)
            {
                if(sujets[i].Id == id)
                {
                    sujet = sujets[i];
                }
            }
            return sujet;
        }

        public List<Sujet> GetAllSujet()
        {
            return this._bddContext.Sujets.ToList();
        }

        public List<Post> GetPosts(int id)
        {
            List<Post> posts = new List<Post>();
            List<Post> allPosts = this._bddContext.Posts.ToList();
            for(int i = 0; i < allPosts.Count(); i++)
            {
                if (allPosts[i].SujetId == id)
                {
                    posts.Add(allPosts[i]);
                }
            }
            return posts;
        }
    }
}
