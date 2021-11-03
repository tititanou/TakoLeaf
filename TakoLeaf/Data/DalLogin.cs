using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Adresse CreationAdresse(string rue, int codePostal, string ville)
        {
            string dep = codePostal.ToString().Substring(0, 2);
            if (dep.Equals("97"))
            {
                dep = codePostal.ToString().Substring(0, 3);
            }
            Adresse adresse = new Adresse { Rue = rue, CodePostal = codePostal, Departement = Int32.Parse(dep), Ville = ville };
            _bddContext.Adresses.Add(adresse);
            _bddContext.SaveChanges();
            return adresse;

        }

      
        public Adherent CreationAdherent(string nom, string prenom, DateTime dateNaissance, int adresseId, string telephone)
        {
            Adherent adherent = new Adherent { Nom = nom, Prenom = prenom, Date_naissance = dateNaissance, AdresseId = adresseId, Telephone = telephone };
            this._bddContext.Adherents.Add(adherent);
            this._bddContext.SaveChanges();
            Historique historique = new Historique { AdherentId = adherent.Id };
            this._bddContext.Historiques.Add(historique);
            this._bddContext.SaveChanges();
            return adherent;
        }

        public CompteUser CreationCompte(string mail, string mdp, string avatar, string description, int adherentId)
        {
            string password = EncodeMD5(mdp);
            CompteUser compteUser = new CompteUser { Mail = mail, MotDePasse = password, Description = description, EtatProfil = EtatProfil.NON_VALIDE, AdherentId = adherentId };
            if(avatar == null)
            {
                compteUser.Avatar = null;
            }

            else
            {
                compteUser.Avatar = avatar;
            }
            this._bddContext.Add(compteUser);
            this._bddContext.SaveChanges();
            return compteUser;
        }

        public Voiture CreationVoiture(string imma, string titulaire, Carburant carburant, int annee, int idmodele, int consumerid)
        {
            Voiture voiture = new Voiture { Immatriculation = imma, Titulaire = titulaire, Carburant = carburant, Annee = annee, ModeleId = idmodele, ConsumerId = consumerid };
            this._bddContext.Voitures.Add(voiture);
            this._bddContext.SaveChanges();
            return voiture;
        }

        public Carte CreationCarte(int idconsumer,string titulaire, string numeroCarte, string date, int crypto)
        {
            Carte carte = new Carte { ConsumerId = idconsumer, NumeroCarte = numeroCarte, Crypto = crypto, ExpirDate = date, Titulaire = titulaire };
            this._bddContext.Add(carte);
            this._bddContext.SaveChanges();
            return carte;
        }

        public Rib CreationRib(string titulaire, string iban, string banque)
        {
            Rib rib = new Rib { Titulaire = titulaire, Iban = iban, Banque = banque };
            _bddContext.Ribs.Add(rib);
            _bddContext.SaveChanges();
            return rib;
        }
        public Consumer CreationConsumer(int idAdherent)
        {
            Consumer consumer = new Consumer { AdherentId = idAdherent };
            this._bddContext.Add(consumer);
            this._bddContext.SaveChanges();
            return consumer;
        }

        public Provider CreationProvider(int idAdherant, int idrib)
        {
            Provider provider = new Provider { Note = 0, AdherentId = idAdherant, RibId = idrib, Rang = Rang.POULPE_AMATEUR };
            _bddContext.Providers.Add(provider);
            _bddContext.SaveChanges();
            return provider;
        }

        public Competence CreationCompetence(double tarifhoraire, int idsscomp,int providerid, string nomssc)
        {
            Competence competence = new Competence { TarifHoraire = tarifhoraire, SsCateCompetenceId = idsscomp, ProviderId = providerid, NomSsCate = nomssc };
            _bddContext.Competences.Add(competence);
            _bddContext.SaveChanges();
            return competence;
        }

        public CompteUser Authentifier(string mail, string mdp)
        {
            string password = EncodeMD5(mdp);
            CompteUser user = _bddContext.CompteUsers.Include(c => c.Adherent).FirstOrDefault(c => c.Mail == mail && c.MotDePasse == password);
            return user;
        }


        public void RoleIsProvider(CompteUser compteUser)
        {
            compteUser.Role = "Provider";
            _bddContext.SaveChanges();
        }

        public void RoleIsConsumer(CompteUser compteUser)
        {
            compteUser.Role = "Consumer";
            _bddContext.SaveChanges();
        }

        public void RoleIsHybride(CompteUser compteUser)
        {
            compteUser.Role = "Hybride";
            _bddContext.SaveChanges();
        }

    

        public string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "TakoLeaf" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        
    }
}
