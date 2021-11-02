using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class AdherentViewModel
    {
        public CompteUser Compte { get; set; }
        public List<PieceJustificative> PieceJusti { get; set; }
        public Provider InfoProvider { get; set; }

    }
}
