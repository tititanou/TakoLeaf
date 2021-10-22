using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalHome : IDalHome
    {

        private BddContext _bddContext;
        
        public void Dispose()
        {
            this._bddContext.Dispose();
        }


    }
}
