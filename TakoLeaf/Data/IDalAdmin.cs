using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Data
{
    interface IDalAdmin : IDisposable
    {

        List<Admin> ObtenirTousLesAdmins();

        Admin ObtenirAdmin(String id);

        List<Adherent> ObtenirTousLesAdherents();

        Adherent ObtenirAdherent(int id);

        List<UtilisateurViewModel> ObtenirTousLesAdherentsEtComptes();

        List<CompteUser> ObtenirTousLesComptesUser();

        void ChangerEtatProfil(int id, int option);
    }
}
