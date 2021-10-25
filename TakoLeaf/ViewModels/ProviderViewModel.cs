﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class ProviderViewModel
    {
        public Adherent Adherent { get; set; }

        public Provider Provider { get; set; }

        public CompteUser CompteUser { get; set; }

        public Competence Competence { get; set; }
        public CateCompetence CateCompetence { get; set; }
        public SsCateCompetence SsCateCompetence { get; set; }
        public List<ProviderCheckBoxViewModel> ListSSC { get; set; }
        public Rib Rib { get; set; }
    }
}
