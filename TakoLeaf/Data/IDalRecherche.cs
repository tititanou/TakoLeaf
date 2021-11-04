using System;
using System.Collections.Generic;
using System.Linq;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public interface IDalRecherche : IDisposable
    {
        bool EstAmi(int id1, int id2);
        List<Adresse> GetAllAdresses();
        List<Adherent> RechercheAdherent(string choix, int code, string nom, string prenom, int competence, string ressource, string input);
        Dictionary<string, List<SsCateCompetence>> RechercheCompetence();
        Dictionary<string, List<int>> RechercheCodePostal();
        CompteUser GetCompteUser(int id);
    }
}
