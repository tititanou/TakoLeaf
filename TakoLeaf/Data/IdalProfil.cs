using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    interface IdalProfil : IDisposable
    {
        List<Adherent> ObtenirAdherents();
        List<CompteUser> ObtenirCompteUser();
        List<Amitie> ObtenirAmities();
        List<DemandeDevis> ObtenirDemandeDevis();
        List<Avis> ObtenirAvis();
        List<DemandeDevisListeRessource> ObtenirRessourceDevis();
        List<HistoriquePresta> ObtenirHistorique();
        public List<DemandeDevisListeCompetence> ObtenirCompetenceDevis();
        void AjoutAmis(Adherent adhrent1, Adherent adherent2);
        void ModifierInfosAdherent(int id, string nom, string prenom, DateTime date, string telephone);
        void ModifierCompteUser(string mail, string mdp, string avatar, string description);
        void ModifierCompetence(int id, double tarif);
        Ressource AjouterRessource(int providerId,string intitule, CateRessource categorie, double tafif, int adresseId);
        void ModifierVoiture(int id, string imma, string titulaire, Carburant carburant, int annee, int idmodele);
        void ModifierRessource(int id, string intitule, CateRessource categorie, double tarif, string rue, int code, string ville);
        void ModifierCarte(int id, string titulaire, string numeroCarte, string date, int crypto);
        void SupprimerVoiture(Voiture voiture);
        void SupprimerCompetence(Competence competence);
        void SupprimerRessource(Ressource ressource);
        void SupprimerCarte(Carte carte);
        List<Voiture> ObtenirVoiture();
        List<Prestation> ObtenirToutesLesPrestations();
        List<Marque> ObtenirMarques();
        List<Carte> ObtenirCartes();
        List<Modele> ObtenirModeles();
        List<Consumer> ObtenirConsumers();
        List<Competence> ObtenirCompetences();
        List<Provider> ObtenirProviders();
        List<SsCateCompetence> ObtenirSSCompetences();
        List<CateCompetence> ObtenirCateCompetences();
        List<Ressource> ObtenirRessources();
        List<Devis> ObtenirDevis();


    }
}
