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
