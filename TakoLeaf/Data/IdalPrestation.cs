using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    interface IdalPrestation : IDisposable
    {
        void ChangerEtatPrestation(int id);
        void AjoutHistorique(Prestation prestation);
        void CreationAvis(int idC, int idP, double note, string contenu, int idPresta);
    }
}
