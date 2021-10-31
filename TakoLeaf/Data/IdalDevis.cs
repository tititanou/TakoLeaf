using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    interface IdalDevis : IDisposable
    {
        DemandeDevis CreationDemandeDevis(int idC, int idP, int idV, DateTime dateDemande, DateTime dateVoulue, string message);
        void CreationListeDevisCompetence(int idC, int idDemandeDevis);

        public void CreationListeDevisRessource(int idR, int idDemandeDevis);

    }
}
