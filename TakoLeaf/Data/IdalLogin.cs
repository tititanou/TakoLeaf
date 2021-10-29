using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    interface IdalLogin : IDisposable
    {


        CompteUser Authentifier(string mail, string mdp);

        Adherent CreationAdherent(string nom, string prenom, DateTime dateNaissance, int adresseId, string telephone);
        CompteUser CreationCompte(string mail, string mdp, string avatar, string description, int adherentId);
        Voiture CreationVoiture(string imma, string titulaire, Carburant carburant, int annee, int idmodele, int consumerid);
        Carte CreationCarte(int idconsumer,string titulaire, string numeroCarte, string date, int crypto);
        Consumer CreationConsumer(int idAdherent);
        Rib CreationRib(string titulaire, string iban, string banque);
        Provider CreationProvider(int idAdherant, int idrib);
        Competence CreationCompetence(double tarifhoraire, int idsscomp,int providerid,string nomssc);
        Adresse CreationAdresse(string rue, int codePostal, string ville);
     

        //public void IsConsumerChecked(Adherent adherent);
        //public void IsProviderChecked(Adherent adherent);
        string EncodeMD5(string motDePasse);

        void RoleIsConsumer(CompteUser compteUser);
        void RoleIsProvider(CompteUser compteUser);
        public void RoleIsHybride(CompteUser compteUser);
    }
}
