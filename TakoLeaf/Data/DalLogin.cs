﻿using System;
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
            string password = EncodeMD5(mdp); // TODO Voir probleme avec l'encodage
            CompteUser compteUser = new CompteUser { Mail = mail, MotDePasse = password, Avatar = avatar, Description = description, EtatProfil = EtatProfil.NON_VALIDE, AdherentId = adherentId };
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

        public Carte CreationCarte(string titulaire, string numeroCarte, string date, int crypto)
        {
            Carte carte = new Carte { NumeroCarte = numeroCarte, Crypto = crypto, ExpirDate = date, Titulaire = titulaire };
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
        public Consumer CreationConsumer(int idAdherent, int idcarte)
        {
            Consumer consumer = new Consumer { AdherentId = idAdherent, CarteId = idcarte };
            this._bddContext.Add(consumer);
            this._bddContext.SaveChanges();
            return consumer;
        }

        public Provider CreationProvider(int idAdherant, int idrib)
        {
            Provider provider = new Provider { Note = 0, AdherentId = idAdherant, RibId = idrib };
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
            CompteUser user = _bddContext.CompteUsers.FirstOrDefault(c => c.Mail == mail && c.MotDePasse == password);
            return user;
        }

        public void IsProviderChecked(Adherent adherent)
        {
            adherent.IsProvider = true;
            _bddContext.SaveChanges();
        }

        public void IsConsumerChecked(Adherent adherent)
        {
            adherent.IsConsumer = true;
            _bddContext.SaveChanges();
        }

        public string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "TakoLeaf" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        
    }
}
