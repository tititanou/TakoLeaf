using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Range(0.0, 5)]
        public double Note { get; set; }

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }

        public int RibId { get; set; }
        public Rib Rib { get; set; }

        public Rang Rang { get; set; }

    }

    public enum Rang
    {
        POULPE_AMATEUR,
        POULPE_BRICOLEUR,
        PIEUVRE_HABILE,
        PIEUVRE_RAFISTOLEUR,
        CALAMAR_RAVALEUR,
        MAITRE_KRAKEN

    }
}
