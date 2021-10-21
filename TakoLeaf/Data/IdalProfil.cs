using System;
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
    }
}
