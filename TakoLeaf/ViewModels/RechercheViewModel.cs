using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using TakoLeaf.Models;

namespace TakoLeaf.ViewModels
{
    public class RechercheViewModel
    {
        [DisplayName("Sous-Categorie")]
        public int SsCateCompetenceId { get; set; }
        public IList<SelectListItem> SsCateList { get; set; }
        public int AdresseId { get; set; }
        public IList<SelectListItem> CodePostauxList { get; set; }
        public CateRessource CateRessource { get; set; }
        public List<Adherent> Adherents { get; set; }
        public List<CompteUser> CompteUsers { get; set; }
    }
}
