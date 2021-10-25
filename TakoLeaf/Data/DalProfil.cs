using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalProfil : IdalProfil
    {
        private BddContext _bddContext;

        public DalProfil()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }
        public List<Adherent> ObtenirAdherents()
        {
            List<Adherent> liste = _bddContext.Adherents.ToList();
            return liste;          
        }

        public List<CompteUser> ObtenirCompteUser()
        {
            List<CompteUser> liste = _bddContext.CompteUsers.ToList();
            return liste;
        }

        public List<Voiture> ObtenirVoiture()
        {
            List<Voiture> liste = _bddContext.Voitures.ToList();
            return liste;
        }

        public List<Marque> ObtenirMarques()
        {
            List<Marque> liste = _bddContext.Marques.ToList();
            return liste;
        }

        public List<Consumer> ObtenirConsumers()
        {
            List<Consumer> liste = _bddContext.Consumers.ToList();
            return liste;
        }

        public List<Carte> ObtenirCartes()
        {
            List<Carte> liste = _bddContext.Cartes.ToList();
            return liste;
        }

        public List<Modele> ObtenirModeles()
        {
            List<Modele> liste = _bddContext.Modeles.Include(m => m.Marque).ToList();
            return liste;
        }

        public List<Competence> ObtenirCompetences()
        {
            List<Competence> liste = _bddContext.Competences.ToList();
            return liste;
        }
        public List<SsCateCompetence> ObtenirSSCompetences()
        {
            List<SsCateCompetence> liste = _bddContext.SsCateCompetences.ToList();
            return liste;
        }
        public List<CateCompetence> ObtenirCateCompetences()
        {
            List<CateCompetence> liste = _bddContext.CateCompetences.ToList();
            return liste;
        }

        public void ModifierInfosAdherent(int id, string nom, string prenom, DateTime date, string adresse, string telephone)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if(adherent!=null)
            {
                adherent.Nom = nom;
                adherent.Prenom = prenom;
                adherent.Date_naissance = date;
                adherent.Adresse = adresse;
                adherent.Telephone = telephone;
                _bddContext.SaveChanges();
            }

        }

        public void ModifierCompteUser(string mail,string mdp, byte[] avatar, string description)
        {
            CompteUser compte = _bddContext.CompteUsers.Find(mail);
            IdalLogin dal = new DalLogin();
            if(compte != null)
            {
                compte.MotDePasse = dal.EncodeMD5(mdp);
                compte.Avatar = avatar;
                compte.Description = description;
                _bddContext.SaveChanges();
            }

        }

    }
}
