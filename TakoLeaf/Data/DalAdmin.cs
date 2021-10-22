using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Data
{
    public class DalAdmin : IDalAdmin
    {
        private BddContext _bddContext;

        public DalAdmin()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public List<Admin> ObtenirTousLesAdmins()
        {
            List<Admin> listeAdmin = this._bddContext.Admins.ToList();
            return listeAdmin;
        }

        public Admin ObtenirAdmin(string id)
        {
            Admin admin = this._bddContext.Admins.Find(id);
            return admin;
        }

        public List<Adherent> ObtenirTousLesAdherents()
        {
            List<Adherent> listeAdherent = this._bddContext.Adherents.ToList();
            return listeAdherent;
        }

        public Adherent ObtenirAdherent(int id)
        {
            Adherent adherent = this._bddContext.Adherents.Find(id);
            return adherent;
        }

        public List<CompteUser> ObtenirTousLesComptesUser()
        {
            List<CompteUser> comptes = this._bddContext.CompteUsers.ToList();
            return comptes;
        }

        public List<UtilisateurViewModel> ObtenirTousLesAdherentsEtComptes()
        {

            List<UtilisateurViewModel> liste = new List<UtilisateurViewModel>();
            List<Adherent> adherents = ObtenirTousLesAdherents().OrderBy(a => a.Id).ToList();
            List<CompteUser> comptes = ObtenirTousLesComptesUser().OrderBy(c => c.AdherentId).ToList();


            for (int i = 0; i < adherents.Count; i++)
            {
                UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherents[i], CompteUser = comptes[i] };
                liste.Add(uvm);
            }

            return liste;
        }

        public void ChangerEtatProfil(int id, int option)
        {
            CompteUser profil = this._bddContext.CompteUsers.Where(c => c.AdherentId == id).FirstOrDefault();


            switch (option)
            {
                case 0:
                    profil.EtatProfil = EtatProfil.VALIDE;
                    this._bddContext.SaveChanges();
                    break;
                case 1:
                    profil.EtatProfil = EtatProfil.COMPTE_BLOQUE;
                    this._bddContext.SaveChanges();
                    break;
                case 2:
                    profil.EtatProfil = EtatProfil.ATTENTE_VALIDATION;
                    this._bddContext.SaveChanges();
                    break;
            }
        }

        public void SupprimerProfil(int id)
        {
            Adherent adherent = ObtenirAdherent(id);
            this._bddContext.Adherents.Remove(adherent);
            this._bddContext.SaveChanges();
        }

        public void AjouterArticle(string titre, string texte)
        {
            DateTime DateDuJour = DateTime.Now;
            Article Article = new Article { Titre = titre, Texte = texte, Date = DateDuJour, Public = false, Image = null };
            // TODO Penser à ajouter l'ID de l'adhérent et image
            this._bddContext.Articles.Add(Article);
            this._bddContext.SaveChanges();
        }

        public List<Article> ObtenirTousLesArticles()
        {
            List<Article> liste = this._bddContext.Articles.ToList();
            return liste;
        }

        public Article ObtenirArticle(int id)
        {
            Article article = this._bddContext.Articles.Find(id);
            return article;
        }
        public Article ModifierArticle(int id, string titre, string texte)
        {
            Article article = ObtenirArticle(id);
            article.Titre = titre;
            article.Texte = texte;
            this._bddContext.Articles.Update(article);
            this._bddContext.SaveChanges();
            return article;
        }

        public void SupprimerArticle(int id)
        {
            Article article = this._bddContext.Articles.Find(id);
            this._bddContext.Articles.Remove(article);
            this._bddContext.SaveChanges();
        }



    }
}
