﻿using System;
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
        void ModifierInfosAdherent(int id, string nom, string prenom, DateTime date, string adresse, string telephone);
        void ModifierCompteUser(string mail, string mdp, byte[] avatar, string description);
        void ModifierCompetence(int id, double tarif);
        Ressource AjouterRessource(int providerId,string intitule, CateRessource categorie, double tafif, string adresse);
        void ModifierVoiture(int id, string imma, string titulaire, Carburant carburant, int annee, int idmodele);
        void ModifierRessource(int id, string intitule, CateRessource categorie, double tarif, string adresse);
        void SupprimerVoiture(Voiture voiture);
        void SupprimerCompetence(Competence competence);
        void SupprimerRessource(Ressource ressource);
        List<Voiture> ObtenirVoiture();
        List<Marque> ObtenirMarques();
        List<Carte> ObtenirCartes();
        List<Modele> ObtenirModeles();
        List<Consumer> ObtenirConsumers();
        List<Competence> ObtenirCompetences();
        List<Provider> ObtenirProviders();
        List<SsCateCompetence> ObtenirSSCompetences();
        List<CateCompetence> ObtenirCateCompetences();
        List<Ressource> ObtenirRessources();



    }
}
