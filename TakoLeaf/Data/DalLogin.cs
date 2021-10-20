using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalLogin : IdalLogin
    {
        private BddContext _bddContext;

        public DalLogin()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public Adherent CreationAdherent(string nom, string prenom, DateTime dateNaissance, string adresse, string telephone)
        {
            Adherent adherent = new Adherent { Nom = nom, Prenom = prenom, Date_naissance = dateNaissance, Adresse = adresse, Telephone = telephone };
            this._bddContext.Adherents.Add(adherent);
            this._bddContext.SaveChanges();
            return adherent;
        }

        public CompteUser CreationCompte(string mail, string mdp, byte[] avatar, string description, int adherentId)
        {
            //string password = EncodeMD5(mdp); TODO Voir probleme avec l'encodage
            CompteUser compteUser = new CompteUser { Mail = mail, MotDePasse = mdp, Avatar = avatar, Description = description, EtatProfil = EtatProfil.NON_VALIDE, AdherentId = adherentId };
            this._bddContext.Add(compteUser);
            this._bddContext.SaveChanges();
            return compteUser;
        }


        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}
