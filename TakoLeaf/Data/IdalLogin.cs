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

        Adherent CreationAdherent(string nom, string prenom, DateTime dateNaissance, string adresse, string telephone);
        CompteUser CreationCompte(string mail, string mdp, byte[] avatar, string description, int adherentId);
        Voiture CreationVoiture(string imma, string titulaire, Carburant carburant, int annee, int idmodele, int consumerid);
        Carte CreationCarte(string titulaire, string numeroCarte, string date, int crypto);
        Consumer CreationConsumer(int idAdherent, int idcarte);
        string EncodeMD5(string motDePasse);
    }
}
