using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalProfil : IdalProfil
    {
        private BddContext _bddContext;

        public DalProfil()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }


        // OBTENTION 

        public List<Adherent> ObtenirAdherents()
        {
            List<Adherent> liste = _bddContext.Adherents.Include(a => a.Adresse).ToList();
            return liste;          
        }

        public List<Avis> ObtenirAvis()
        {
            List<Avis> liste = _bddContext.Avis.Include(a => a.Prestation).ThenInclude(a => a.Consumer).ThenInclude(a => a.Adherent).Include(a => a.Prestation).ToList();
            return liste;
        }

        public List<CompteUser> ObtenirCompteUser()
        {
            List<CompteUser> liste = _bddContext.CompteUsers.ToList();
            return liste;
        }

        public List<Voiture> ObtenirVoiture()
        {
            List<Voiture> liste = _bddContext.Voitures.Include(v => v.Modele).ThenInclude(v => v.Marque).ToList();
            return liste;
        }

        public List<Marque> ObtenirMarques()
        {
            List<Marque> liste = _bddContext.Marques.ToList();
            return liste;
        }

        public List<Consumer> ObtenirConsumers()
        {
            List<Consumer> liste = _bddContext.Consumers.Include(p => p.Adherent).ThenInclude(p => p.Adresse).ToList();
            return liste;
        }

        public List<Carte> ObtenirCartes()
        {
            List<Carte> liste = _bddContext.Cartes.ToList();
            return liste;
        }

        public List<Modele> ObtenirModeles()
        {
            List<Modele> liste = _bddContext.Modeles.Include(m => m.Marque).ToList();
            return liste;
        }

        public List<Devis> ObtenirDevis()
        {
            List<Devis> liste = _bddContext.Devis.Include(d => d.LieuPresta).Include(d => d.Consumer).ThenInclude(d => d.Adherent).Include(d => d.Provider).ThenInclude(d => d.Adherent).Include(d => d.Voiture).ThenInclude(d => d.Modele).ThenInclude(d => d.Marque).Include(d => d.LieuPresta).Include(d => d.DemandeDevis).ToList();
            return liste;
        }
        public List<DemandeDevis> ObtenirDemandeDevis()
        {
            List<DemandeDevis> liste = _bddContext.DemandeDevis.ToList();
            return liste;
        }

        public List<DemandeDevisListeCompetence> ObtenirCompetenceDevis()
        {
            List<DemandeDevisListeCompetence> liste = _bddContext.DemandesDevisListeCompetence.Include(c => c.Competence).ToList();
            return liste;
        }

        public List<DemandeDevisListeRessource> ObtenirRessourceDevis()
        {
            List<DemandeDevisListeRessource> liste = _bddContext.DemandesDevisListeRessource.Include(r => r.Ressource).ToList();
            return liste;
        }
        public List<Amitie> ObtenirAmities()
        {
            List<Amitie> liste = _bddContext.Amities.ToList();
            return liste;
        }

        public List<Competence> ObtenirCompetences()
        {
            List<Competence> liste = _bddContext.Competences.Include(c => c.SsCateCompetence).Include(c => c.Provider).ThenInclude(c => c.Adherent).ToList();
            return liste;
        }
        public List<SsCateCompetence> ObtenirSSCompetences()
        {
            List<SsCateCompetence> liste = _bddContext.SsCateCompetences.ToList();
            return liste;
        }
        public List<CateCompetence> ObtenirCateCompetences()
        {
            List<CateCompetence> liste = _bddContext.CateCompetences.ToList();
            return liste;
        }

        public List<Ressource> ObtenirRessources()
        {
            List<Ressource> liste = _bddContext.Ressources.ToList();
            return liste;

        }
           
        public List<Provider> ObtenirProviders()
        {
            List<Provider> liste = _bddContext.Providers.Include(p =>p.Adherent).ThenInclude(p =>p.Adresse).ToList();
            return liste;
        }

        public List<Prestation> ObtenirToutesLesPrestations()
        {
            List<Prestation> prestations = this._bddContext.Prestations.Include(p => p.Consumer).ThenInclude(p => p.Adherent).ThenInclude(p => p.Adresse).Include(p => p.Provider).ThenInclude(p => p.Adherent).ThenInclude(p => p.Adresse).OrderByDescending(p => p.DateDebut).ToList();
            return prestations;
        }
        // MODIFICATION

        public void ModifierInfosAdherent(int id, string nom, string prenom, DateTime date, string telephone)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if(adherent!=null)
            {
                adherent.Nom = nom;
                adherent.Prenom = prenom;
                adherent.Date_naissance = date;
                adherent.Telephone = telephone;
                _bddContext.SaveChanges();
            }

        }

        public void ModifierAdresse(int id, string rue, int codePostal, string ville)
        {
            Adresse adresse = _bddContext.Adresses.Find(id);
            adresse.Rue = rue;
            adresse.CodePostal = codePostal;
            adresse.Ville = ville;
            _bddContext.SaveChanges();
        }

        public void ModifierCompteUser(string mail,string mdp, string avatar, string description)
        {
            CompteUser compte = _bddContext.CompteUsers.Find(mail);
            IdalLogin dal = new DalLogin();
            if(compte != null)
            {
                compte.MotDePasse = dal.EncodeMD5(mdp);
                compte.Avatar = avatar;
                compte.Description = description;
                _bddContext.SaveChanges();
            }

        }

        public void ModifierCompetence(int id, double tarif)
        {
            Competence competence = _bddContext.Competences.Find(id);
            competence.TarifHoraire = tarif;
            _bddContext.SaveChanges();

        }

        public void ModifierVoiture(int id, string imma, string titulaire, Carburant carburant, int annee, int idmodele)
        {
            Voiture voiture = _bddContext.Voitures.Find(id);
            voiture.Immatriculation = imma;
            voiture.Titulaire = titulaire;
            voiture.Carburant = carburant;
            voiture.Annee = annee;
            voiture.ModeleId = idmodele;
            _bddContext.SaveChanges();

        }

        public void ModifierRessource (int id, string intitule, CateRessource categorie, double tarif, string rue, int code, string ville)
        {
            Ressource ressource = _bddContext.Ressources.Find(id);
            ressource.Intitule = intitule;
            ressource.Adresse.Rue = rue;
            ressource.Adresse.CodePostal = code;
            ressource.Adresse.Ville = ville;
            ressource.Categorie = categorie;
            ressource.TarifJournalier = tarif;
            _bddContext.SaveChanges();

        }

        public void ModifierCarte (int id, string titulaire, string numeroCarte, string date, int crypto)
        {
            Carte carte = _bddContext.Cartes.Find(id);
            carte.Titulaire = titulaire;
            carte.NumeroCarte = numeroCarte;
            carte.ExpirDate = date;
            carte.Crypto = crypto;
            _bddContext.SaveChanges();
        }
        // AJOUT

        public Ressource AjouterRessource(int providerId, string intitule, CateRessource categorie, double tarif, int adresseId)
        {
            Ressource ressource = new Ressource { Intitule = intitule, AdresseId = adresseId, Categorie = categorie, Disponible = true, ProviderId = providerId, TarifJournalier = tarif };
            _bddContext.Ressources.Add(ressource);
            _bddContext.SaveChanges();
            return ressource;
        }

       

        // SUPPRESSION

        public void SupprimerCompetence(Competence competence)
        {
            _bddContext.Competences.Remove(competence);
            _bddContext.SaveChanges();

        }
        // TODO ajouter la methode SUPPRIMER         

        
        public void SupprimerVoiture(Voiture voiture)
        {

            _bddContext.Voitures.Remove(voiture);
            _bddContext.SaveChanges();
        }

        public void SupprimerRessource(Ressource ressource)
        {
            _bddContext.Ressources.Remove(ressource);
            _bddContext.SaveChanges();
        }

        public void SupprimerCarte(Carte carte)
        {
            _bddContext.Cartes.Remove(carte);
            _bddContext.SaveChanges();
        }

        // AMIS / BLOQUES

        public void AjoutAmis(Adherent adherent1, Adherent adherent2)
        {
            Amitie amitie = new Amitie { AdherentCourantId = adherent2.Id, AdherentAmiId = adherent1.Id };
            _bddContext.Amities.Add(amitie);
            _bddContext.SaveChanges();
            
        }

        // Historique

        public List<HistoriquePresta> ObtenirHistorique() 
        { 
            List<HistoriquePresta> historiques = _bddContext.HistoriquePrestas.Include(p => p.Prestation).ThenInclude(p => p.Consumer).ThenInclude(p => p.Adherent).ThenInclude(p => p.Adresse).Include(p =>p.Prestation).ThenInclude(p=>p.Devis).Include(p => p.Prestation.Provider).ThenInclude(p => p.Adherent).ThenInclude(p => p.Adresse).Include(p =>p.Prestation).ThenInclude(p=>p.Voiture).ThenInclude(p=>p.Modele).ThenInclude(p => p.Marque).ToList();
            return historiques;
        }
       
    }
}
