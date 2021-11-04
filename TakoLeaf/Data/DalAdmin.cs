using Microsoft.EntityFrameworkCore;
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

        public List<CompteUser> ObtenirTousLesAdherentsEtComptes()
        {

            List<CompteUser> Ad = this._bddContext.CompteUsers.Include(a => a.Adherent).ToList();

            //List<UtilisateurViewModel> liste = new List<UtilisateurViewModel>();
            //List<Adherent> adherents = ObtenirTousLesAdherents().OrderBy(a => a.Id).ToList();
            //List<CompteUser> comptes = ObtenirTousLesComptesUser().OrderBy(c => c.AdherentId).ToList();


            //for (int i = 0; i < adherents.Count; i++)
            //{
            //    UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherents[i], CompteUser = comptes[i] };
            //    liste.Add(uvm);
            //}

            return Ad;
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
                case 3:
                    profil.EtatProfil = EtatProfil.NON_VALIDE;
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

        public void AjouterArticle(string titre, string texte, bool visibilite)
        {
            DateTime DateDuJour = DateTime.Now;
            Article article = new Article { Titre = titre, Texte = texte, DateRedaction = DateDuJour, Image = null };

            if (visibilite)
            {
                article.Public = true;
            }
            else
            {
                article.DatePublication = DateDuJour;
                article.Public = false;
            }

            // TODO Penser à ajouter l'ID de l'adhérent et image
            this._bddContext.Articles.Add(article);
            this._bddContext.SaveChanges();
        }

        public void PublierArticle(Article article)
        {
            this.AjouterArticle(article.Titre, article.Texte, article.Public = true);

            article.DatePublication = DateTime.Now;

            this._bddContext.Articles.Update(article);
            this._bddContext.SaveChanges();

        }


        public List<Article> ObtenirTousLesArticles()
        {
            List<Article> liste = this._bddContext.Articles.OrderBy(a => a.Id).ToList();
            return liste;
        }

        public List<Article> ObtenirTousLesArticlesPublic()
        {

            // Attention, les articles sont récupérés par
            // ordre décroissant en date pour être affiché
            // dans l'ordre dans le fil d'actualité
            List<Article> liste = this._bddContext.Articles.OrderByDescending(a => a.DatePublication).Where(a => a.Public == true).ToList();
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

        public void ModifierVisibiliteArticle(int id)
        {
            Article article = this._bddContext.Articles.Where(a => a.Id == id).FirstOrDefault();

            if (article.Public)
            {
                article.Public = false;

            }
            else
            {
                if (article.DatePublication != null)
                {
                    article.DatePublication = DateTime.Now;
                }
                article.Public = true;
            }

            this._bddContext.Articles.Update(article);
            this._bddContext.SaveChanges();
        }

        public List<Prestation> ObtenirToutesLesPrestations()
        {
            List<Prestation> prestations = this._bddContext.Prestations.Include(p => p.Consumer.Adherent).Include(p => p.Provider.Adherent).OrderByDescending(p => p.DateDebut).ToList();
            return prestations;
        }

        public List<Prestation> ObtenirPrestationsParProvider(int providerId)
        {
            List<Prestation> prestations = this._bddContext.Prestations.Where(p => p.ProviderId == providerId).ToList();
            return prestations;
        }

        public List<CompteUser> ObtenirAdherentsEtComptes()
        {
            List<CompteUser> liste = this._bddContext.CompteUsers.Include(p => p.Adherent).ToList();
            return liste;
        }

        public Prestation ObtenirPrestation(int idPrestation)
        {
            //Recupération Prestation avec le consumer, le provider, et la voiture associée

            Prestation prestation = this._bddContext.Prestations.Where(p => p.Id == idPrestation)
                .Include(p => p.Consumer.Adherent)
                //.ThenInclude(p => p.Adherent)
                .Include(p => p.Provider.Adherent)
                //.ThenInclude(p => p.Adherent)
                .Include(p => p.Voiture)
                .FirstOrDefault();
            return prestation;
        }

        public CompteUser ObtenirCompteUser(int idAdherent)
        {
            CompteUser compte = this._bddContext.CompteUsers.Where(p => p.AdherentId == idAdherent).FirstOrDefault();
            return compte;
        }

        public Provider ObtenirRib(int adherentId)
        {
            Provider rib = this._bddContext.Providers.Include(p => p.Rib).Where(p => p.AdherentId == adherentId).FirstOrDefault();
            return rib;
        }



        //à voir si on a le temps

        //public void ValiderTransaction(int idPrestation)
        //{
        //    Prestation prestation = this._bddContext.Prestations.Where(p => p.Id == idPrestation).FirstOrDefault();
        //    prestation.EtatPresta = Prestation.Etat.Valide;
        //    this._bddContext.Prestations.Update(prestation);
        //    this._bddContext.SaveChanges();

        //}


        public CompteUser ObtenirAdherentEtCompte(int idAdherent)
        {
            CompteUser ad = this._bddContext.CompteUsers.Where(a => a.AdherentId == idAdherent).Include(a => a.Adherent).FirstOrDefault();
            return ad;
        }

        public List<PieceJustificative> ObtenirPieceJustificative(int IdAdherent)
        {
            List<PieceJustificative> liste = this._bddContext.PieceJustificatives.Where(p => p.AdherentId == IdAdherent).ToList();
            return liste;
        }

        public List<PostSignale> ObtenirLesPostesSignales()
        {
            List<PostSignale> liste = this._bddContext.PostSignales.Include(p => p.AdherentSignalant).Include(p => p.AdherentSignale).Include(p => p.Post).ToList();
            return liste;
        }

        public Provider ObtenirProvider(int id)
        {
            Provider provider = this._bddContext.Providers.Where(p => p.AdherentId == id).Include(p => p.Rib).FirstOrDefault();
            return provider;

        }

        public void ValiderTransaction(int id)
        {
            Prestation prestation = this._bddContext.Prestations.Find(id);
            prestation.EtatPresta = Prestation.Etat.Cloture;
            this._bddContext.SaveChanges();
        }

    }
}
