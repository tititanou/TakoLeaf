using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Data
{
    public class DalAdmin : IDalAdmin
    {
        private BddContext _bddContext;

        public DalAdmin()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public List<Admin> ObtenirTousLesAdmins()
        {
            List<Admin> listeAdmin = this._bddContext.Admins.ToList();
            return listeAdmin;
        }

        public Admin ObtenirAdmin(string id)
        {
            Admin admin = this._bddContext.Admins.Find(id);
            return admin;
        }

        public List<Adherent> ObtenirTousLesAdherents()
        {
            List<Adherent> listeAdherent = this._bddContext.Adherents.ToList();
            return listeAdherent;
        }

        public Adherent ObtenirAdherent(int id)
        {
            Adherent adherent = this._bddContext.Adherents.Find(id);
            return adherent;
        }

        public List<CompteUser> ObtenirTousLesComptesUser()
        {
            List<CompteUser> comptes = this._bddContext.CompteUsers.ToList();
            return comptes;
        }

        public List<UtilisateurViewModel> ObtenirTousLesAdherentsEtComptes()
        {

            List<UtilisateurViewModel> liste = new List<UtilisateurViewModel>();
            List<Adherent> adherents = ObtenirTousLesAdherents().OrderBy(a => a.Id).ToList();
            List<CompteUser> comptes = ObtenirTousLesComptesUser().OrderBy(c => c.AdherentId).ToList();


            for (int i = 0; i < adherents.Count; i++)
            {
                UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherents[i], CompteUser = comptes[i] };
                liste.Add(uvm);
            }

            return liste;
        }

        public void ChangerEtatProfil(int id, int option)
        {
            CompteUser profil = this._bddContext.CompteUsers.Where(c => c.AdherentId == id).FirstOrDefault();


            switch (option)
            {
                case 1:
                    profil.EtatProfil = EtatProfil.VALIDE;
                    this._bddContext.SaveChanges();
                    break;
                case 2:
                    profil.EtatProfil = EtatProfil.COMPTE_BLOQUE;
                    this._bddContext.SaveChanges();
                    break;
            }


        }




    }
}
