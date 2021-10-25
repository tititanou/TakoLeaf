using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            bool ok = false;
            List<Sujet> sujets = this._bddContext.Sujets.ToList();
            for (int i = 0; i < sujets.Count(); i++)
            {
                if (sujets[i].Titre.ToLower().Equals(sujet.Titre.ToLower()))
                {
                    ok = false;
                    return;
                }
                else
                {
                    ok = true;
                }
            }
            if (ok)
            {
                this._bddContext.Sujets.Add(sujet);
                this._bddContext.SaveChanges();
            }
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

        public Post Get1Post(int id)
        {
            Post post = null;
            List<Post> posts = this._bddContext.Posts.ToList();
            for (int i = 0; i < posts.Count(); i++)
            {
                if (posts[i].Id == id)
                {
                    post = posts[i];
                }
            }
            return post;
        }

        public List<Sujet> GetAllSujets()
        {
            return this._bddContext.Sujets.ToList();
        }

        public List<Post> GetPosts(int id)
        {
            List<Post> posts = new List<Post>();
            List<Post> allPosts = this._bddContext.Posts.Include(p => p.Adherent).ToList();
            for(int i = 0; i < allPosts.Count(); i++)
            {
                if (allPosts[i].SujetId == id)
                {
                    posts.Add(allPosts[i]);
                }
            }
            return posts;
        }

        public void SuppressionSujet(Sujet sujet)
        {
            this._bddContext.Remove(sujet);
            this._bddContext.SaveChanges();
        }

        public void SuppressionPost(Post post)
        {
            this._bddContext.Remove(post);
            this._bddContext.SaveChanges();
        }

        public void ModificationSujet(Sujet sujet)
        {
            this._bddContext.Update(sujet);
            this._bddContext.SaveChanges();
        }

        public void ModificationPost(Post post)
        {
            this._bddContext.Update(post);
            this._bddContext.SaveChanges();
        }

        public Sujet RechercheSujetParTitre(string titre)
        {
            List<Sujet> sujets = this._bddContext.Sujets.ToList();
            Sujet sujet = null;
            for(int i = 0; i < sujets.Count(); i++)
            {
                if (sujets[i].Titre.Equals(titre))
                {
                    sujet = sujets[i];
                }
            }
            return sujet;
        }
    }
}