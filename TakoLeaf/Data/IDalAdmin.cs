using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Data
{
    interface IDalAdmin : IDisposable
    {

        List<Admin> ObtenirTousLesAdmins();
        Admin ObtenirAdmin(String id);
        List<Adherent> ObtenirTousLesAdherents();
        Adherent ObtenirAdherent(int id);
        List<CompteUser> ObtenirTousLesAdherentsEtComptes();
        List<CompteUser> ObtenirTousLesComptesUser();
        void ChangerEtatProfil(int id, int option);
        void SupprimerProfil(int id);
        void AjouterArticle(string titre, string texte, bool visibilite);
        List<Article> ObtenirTousLesArticles();
        List<Article> ObtenirTousLesArticlesPublic();
        Article ObtenirArticle(int id);
        Article ModifierArticle(int id, string titre, string texte);
        void SupprimerArticle(int id);
        void ModifierVisibiliteArticle(int id);
        void PublierArticle(Article article);
        List<Prestation> ObtenirToutesLesPrestations();
        List<Prestation> ObtenirPrestationsParProvider(int providerId);
        List<CompteUser> ObtenirAdherentsEtComptes();
        Prestation ObtenirPrestation(int idPrestation);
        CompteUser ObtenirCompteUser(int idAdherent);
        Provider ObtenirRib(int adherentId);
        CompteUser ObtenirAdherentEtCompte(int idAdherent);
        List<PieceJustificative> ObtenirPieceJustificative(int IdAdherent);
        List<PostSignale> ObtenirLesPostesSignales();
        Provider ObtenirProvider(int id);
        void ValiderTransaction(int id);
    }
}
