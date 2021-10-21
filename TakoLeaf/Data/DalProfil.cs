using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalProfil : IdalProfil
    {
        private BddContext _bddContext;

        public DalProfil()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }
        public List<Adherent> ObtenirAdherents()
        {
            List<Adherent> liste = _bddContext.Adherents.ToList();
            return liste;          
        }

        public List<CompteUser> ObtenirCompteUser()
        {
            List<CompteUser> liste = _bddContext.CompteUsers.ToList();
            return liste;
        }
            
    }
}
