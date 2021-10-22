﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    interface IdalProfil : IDisposable
    {
        List<Adherent> ObtenirAdherents();
        List<CompteUser> ObtenirCompteUser();

        void ModifierInfosAdherent(int id, string nom, string prenom, DateTime date, string adresse, string telephone);
        void ModifierCompteUser(string mail, string mdp, byte[] avatar, string description);
    }
}