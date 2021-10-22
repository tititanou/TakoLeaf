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
        string EncodeMD5(string motDePasse);
    }
}
