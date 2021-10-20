﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class Consumer
    {
       
        public int Id { get; set; }
       

        public int AdherentId { get; set; }
        public Adherent Adherent { get; set; }
       

        public int VoitureId { get; set; }
        public Voiture Voiture { get; set; }


        public int CarteId { get; set; }
        public Carte Carte { get; set; }

        public Etat etatProfil { get; set; }
        public enum Etat
        {
            Valide,
            Non_Valide,
            En_Attente_De_Validation
        }



    }
}
