using System;
using System.Collections.Generic;
using System.Linq;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalRecherche : IDalRecherche
    {
        private BddContext _bddContext;

        public DalRecherche()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public bool EstAmi(int id1, int id2)
        {
            DalProfil dalProfil = new DalProfil();
            List<Amitie> amities = dalProfil.ObtenirAmities().Where(a => a.AdherentCourantId == id2).ToList();
            bool res = false;
            foreach (var item in amities)
            {
                if (item.AdherentAmiId == id1)
                {
                    res = true;
                    break;
                }

                else
                {
                    res = false;
                }
            }

            return res;
        }
    }
}
