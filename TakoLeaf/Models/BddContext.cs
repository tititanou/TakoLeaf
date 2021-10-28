﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;

namespace TakoLeaf.Models
{
    public class BddContext : DbContext
    {

        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Devis> Devis { get; set; }
        public DbSet<CompteUser> CompteUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<PieceJustificative> PieceJustificatives { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Sujet> Sujets { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Carte> Cartes { get; set; }
        public DbSet<Historique> Historiques { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public DbSet<Avis> Avis { get; set; }
        public DbSet<DemandeDevis> DemandeDevis { get; set; }
        public DbSet<CateCompetence> CateCompetences { get; set; }
        public DbSet<SsCateCompetence> SsCateCompetences { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Rib> Ribs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<HistoriquePresta> HistoriquePrestas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoriquePresta>(entity =>
            {

                entity.HasKey(e => new { e.HistoriqueId, e.PrestationId });
            });

        }


        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Admins.Add(
                new Admin
                {
                    Id = "admin",
                    Pwd = "toto"
                });

            // TODO Voir pour mettre le DELETE ON CASCADE sur certaines clés etrangeres

            DalLogin dal = new DalLogin();
            this.Adherents.AddRange(
                new Adherent
                {
                    Id = 1,
                    Nom = "ABRATE",
                    Prenom = "Alexis",
                    Date_naissance = new DateTime(1990, 10, 23),
                    Adresse = "Ripas",
                    Telephone = "0658423947"

                },

                 new Adherent
                 {
                     Id = 2,
                     Nom = "ROJAS",
                     Prenom = "Tania",
                     Date_naissance = new DateTime(1980, 02, 10),
                     Adresse = "Ripas",
                     Telephone = "0693562410"
                 },

                 new Adherent
                 {
                     Id = 3,
                     Nom = "ZAWARTOSKI",
                     Prenom = "Valentin",
                     Date_naissance = new DateTime(1994, 12, 01),
                     Adresse = "Ripas",
                     Telephone = "0652471230"
                 },

                new Adherent
                {
                    Id = 4,
                    Nom = "DAUPHIN",
                    Prenom = "Anthony",
                    Date_naissance = new DateTime(1993, 08, 10),
                    Adresse = "Nayto",
                    Telephone = "0626576356"
                },



                new Adherent
                {
                    Id = 5,
                    Nom = "Guillaume",
                    Prenom = "Levy",
                    Date_naissance = new DateTime(1990, 07, 18),
                    Adresse = "Lyon",
                    Telephone = "0618525231",

                },

                new Adherent
                {
                    Id = 6,
                    Nom = "Andre",
                    Prenom = "Soulard",
                    Date_naissance = new DateTime(1955, 12, 12),
                    Adresse = "Paris",
                    Telephone = "0699885544"

                },

                 new Adherent
                 {
                     Id = 7,
                     Nom = "Gaston",
                     Prenom = "Lagaffe",
                     Date_naissance = new DateTime(1965, 03, 15),
                     Adresse = "Paris",
                     Telephone = "0699885544"

                 },

                  new Adherent
                  {
                      Id = 8,
                      Nom = "Jean",
                      Prenom = "Neymar",
                      Date_naissance = new DateTime(1967, 10, 09),
                      Adresse = "Paris",
                      Telephone = "0699885544"

                  },

                  new Adherent
                  {
                      Id = 9,
                      Nom = "Prunelle",
                      Prenom = "Demaizieux",
                      Date_naissance = new DateTime(1980, 04, 22),
                      Adresse = "Paris",
                      Telephone = "0699885544"

                  }
                );

            this.CompteUsers.AddRange(
                 new CompteUser
                 {
                     Mail = "Alexis.Abrate@gmail.com",
                     MotDePasse = dal.EncodeMD5("LeTruantDuCSharp"),
                     Description = "Hello",
                     EtatProfil = EtatProfil.VALIDE,
                     AdherentId = 1

                 },

                  new CompteUser
                  {
                      Mail = "Rojas.Tania@gmail.com",
                      MotDePasse = dal.EncodeMD5("LaMama"),
                      Description = "MacPowa",
                      EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                      AdherentId = 2,
                      Role = "Consumer"

                  },
                  new CompteUser
                  {
                      Mail = "Zawartoski.Valentin@gmail.com",
                      MotDePasse = dal.EncodeMD5("ZeFrontiste"),
                      Description = "Ma RAM rame...",
                      EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                      AdherentId = 3,
                      Role = "Consumer"
                  },
                   new CompteUser
                   {
                       Mail = "Anthony.Dauphin@gmail.com",
                       MotDePasse = dal.EncodeMD5("ElBrutos"),
                       Description = "I HAVE NO IDEA WHAT I'M DOING",
                       EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                       AdherentId = 4,
                       Role = "Admin"

                   },

                   new CompteUser
                   {
                       Mail = "provider@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "C'est moi le Provider Test",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 5,
                       Role = "Provider"

                   },

                   new CompteUser
                   {
                       Mail = "consumer@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "C'est moi le consumer Test",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 6,
                       Role = "Consumer"
                   },

                   new CompteUser
                   {
                       Mail = "Lagaffe@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "M'enfin ?!",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 7,
                       Role = "Provider"
                   },

                   new CompteUser
                   {
                       Mail = "Naymar@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "Ah ! Get it ?",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 8,
                       Role = "Consumer"
                   },

                   new CompteUser
                   {
                       Mail = "Demaizieux@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "Vous avez saisi mon calembours ?",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 9,
                       Role = "Consumer"
                   }

                   );

            this.Consumers.AddRange(

                new Consumer
                {
                    Id = 1,
                    AdherentId = 6,
                    CarteId = 1
                    
                },

                 new Consumer
                 {
                     Id = 2,
                     AdherentId = 9,
                     CarteId = 2
                 },

                  new Consumer
                  {
                      Id = 3,
                      AdherentId = 8,
                      CarteId = 3
                  },

                   new Consumer
                   {
                       Id = 4,
                       AdherentId = 3,
                       CarteId = 4
                   }




                );
            this.Cartes.AddRange(
                new Carte
                {
                    Id=1,
                    Titulaire = "MR SOULARD",
                    NumeroCarte = "1578323524729856",
                    ExpirDate = "08/2023",
                    Crypto = 458
                },

                new Carte
                {
                    Id = 2,
                    Titulaire = "MR DEMAIZIEUX",
                    NumeroCarte = "1234567891012023",
                    ExpirDate = "10/2023",
                    Crypto = 246
                },

                new Carte
                {
                    Id = 3,
                    Titulaire = "MR NAYMAR",
                    NumeroCarte = "1415242536359695",
                    ExpirDate = "01/2024",
                    Crypto = 666
                },

                 new Carte
                 {
                     Id = 4,
                     Titulaire = "MR ZAWARTOSKI",
                     NumeroCarte = "878596832361412",
                     ExpirDate = "05/2022",
                     Crypto = 789
                 }


                );

            this.Providers.AddRange(
                new Provider
                {
                    Id = 1,
                    Note = 0,
                    RibId = 1,
                    AdherentId = 5
                },

                 new Provider
                 {
                     Id = 2,
                     Note = 0,
                     RibId = 2,
                     AdherentId = 7
                 }
                );

            this.Ribs.AddRange(
                new Rib
                {
                    Id = 1,
                    Titulaire = "Guillaume Levy",
                    Iban = "1245 1245 1245 1245",
                    Banque = "Boursorama"

                },

                 new Rib
                 {
                     Id = 2,
                     Titulaire = "Gaston Lagaffe",
                     Iban = "1412 3236 9698 7487",
                     Banque = "Caisse d'Epargne"

                 }
                );

            this.Voitures.AddRange(

                new Voiture
                {
                    Id = 1,
                    Annee = 2004,
                    Carburant = Carburant.DIESEL,
                    ConsumerId = 1,
                    Immatriculation = "CA-175-AA",
                    Titulaire = "MR SOULARD",
                    ModeleId = 2

                },

                new Voiture
                {
                    Id = 2,
                    Annee = 2017,
                    Carburant = Carburant.SP98,
                    ConsumerId = 1,
                    Immatriculation = "BB-585-DF",
                    Titulaire = "MME SOULARD",
                    ModeleId = 5
                },

                new Voiture
                {
                    Id = 3,
                    Annee = 2015,
                    Carburant = Carburant.DIESEL,
                    ConsumerId = 3,
                    Immatriculation = "BD-524-AV",
                    Titulaire = "MR NEYMAR",
                    ModeleId = 5
                },

                new Voiture
                {
                    Id = 4,
                    Annee = 2010,
                    Carburant = Carburant.GPL,
                    ConsumerId = 2,
                    Immatriculation = "AJ-526-NN",
                    Titulaire = "MR DEMAIZIEUX",
                    ModeleId = 5
                }


                );

            this.Sujets.AddRange(
                new Sujet
                {
                    Id = 1,
                    Date = new DateTime(2021, 07, 26, 14, 51, 03),
                    Titre = "Problème Essuie-Glaces Citroën C4 Picasso"
                },
                new Sujet
                {
                    Id = 2,
                    Date = new DateTime(2021, 08, 05, 23, 22, 58),
                    Titre = "Peinture Chrysler Grand Voyageur"
                });
            this.Posts.AddRange(
                new Post
                {
                    Id = 1,
                    Date = new DateTime(2021, 07, 26, 14, 51, 03),
                    CorpsPost = "Salut, j'ai un pb avec Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec faucibus efficitur fermentum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec volutpat nunc quis tellus lacinia.",
                    SujetId = 1,
                    AdherentId = 1
                },
                new Post
                {
                    Id = 2,
                    Date = new DateTime(2021, 07, 26, 21, 35, 35),
                    CorpsPost = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tempor ipsum et lectus convallis venenatis. Morbi ex sapien, cursus sed.",
                    SujetId = 1,
                    AdherentId = 3
                },
                new Post
                {
                    Id = 3,
                    Date = new DateTime(2021, 08, 05, 18, 14, 02),
                    CorpsPost = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent consequat dolor sed imperdiet consectetur. Pellentesque sed ante eu dolor vulputate lacinia. Praesent vitae lorem ultrices, cursus justo vestibulum, bibendum sem. Etiam sit amet magna libero. Fusce in ex quam. Aenean.",
                    SujetId = 2,
                    AdherentId = 2
                }, new Post
                {
                    Id = 4,
                    Date = new DateTime(2021, 08, 06, 19, 01, 55),
                    CorpsPost = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec mattis lobortis vehicula. Proin sollicitudin suscipit augue, fermentum dictum lacus pharetra eu. Mauris ac massa at ipsum laoreet dapibus. Donec volutpat urna vitae accumsan fermentum. Interdum.",
                    SujetId = 1,
                    AdherentId = 2
                }, new Post
                {
                    Id = 5,
                    Date = new DateTime(2021, 08, 15, 08, 05, 27),
                    CorpsPost = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent eu ante vitae nunc aliquam ornare.",
                    SujetId = 2,
                    AdherentId = 1
                }
                );

            this.Marques.AddRange(
                new Marque { Nom = "Volkswagen" },
                new Marque { Nom = "Peugeot" },
                new Marque { Nom = "Audi"}
                );

            this.Modeles.AddRange(
                new Modele { Nom = "Polo", MarqueId = 1 },
                new Modele { Nom = "Golf", MarqueId = 1 },
                new Modele { Nom = "206", MarqueId = 2 },
                new Modele { Nom = "607", MarqueId = 2 },
                new Modele { Nom = "A2", MarqueId = 3 },
                new Modele { Nom = "A4", MarqueId = 3}
                );

            this.CateCompetences.AddRange(
                new CateCompetence { Intitule = "Pneu" },
                new CateCompetence { Intitule = "Moteur"}
                );

            this.SsCateCompetences.AddRange(
                new SsCateCompetence { Intitule = "Changer Roue", CateCompetenceId = 1 },
                new SsCateCompetence { Intitule = "Changer Pneu", CateCompetenceId = 1 },
                new SsCateCompetence { Intitule = "Changer la distribution", CateCompetenceId = 2 },
                new SsCateCompetence { Intitule = "Changer les viblequins", CateCompetenceId = 2 }
                );
            this.Competences.AddRange(
                new Competence { Id = 1, ProviderId = 1, SsCateCompetenceId = 1, NomSsCate = "Changer Roue", TarifHoraire = 28 },
                new Competence { Id = 2, ProviderId = 1, SsCateCompetenceId = 3, NomSsCate = "Changer la distribution", TarifHoraire = 47 }

                );

            



            this.Articles.AddRange(
                new Article
                {
                    AdminId = "Admin",
                    Titre = "Lancement d'une plateforme révolutionnaire en TakoLeaf' !",
                    DateRedaction = new DateTime(2021, 10, 19),
                    DatePublication = new DateTime(2021, 10, 24),
                    Texte = "Marre de payer trop cher pour votre voiture adorée ? Alors vous êtes tombé au bon endroit ! Bon vous allez payer quand même certes... mais sur HTML codé main !",
                    Public = true
                },

                 new Article
                 {
                     AdminId = "Admin",
                     Titre = "Flash Spécial !",
                     DateRedaction = new DateTime(2021, 10, 22),
                     DatePublication = new DateTime(2021, 10, 23),
                     Texte = "M.ABRATE Alexis, codeur de son état aurait été aperçu chez lui, tenant des propos choquants sur la bonne moralité de la maman de sa DropDownList !",
                     Public = true
                 },

                  new Article
                  {
                      AdminId = "Admin",
                      Titre = "Bientôt un peu moins moche ?",
                      DateRedaction = new DateTime(2021,10,20),
                      DatePublication = new DateTime(2021,10,21),
                      Texte = "Les dernières rumeurs suggèrent que notre spécialiste du Front, M.ZAWARTOSKI Valentin serai sur le point de révolutionner le CSS !",
                      Public = false
                  }
                );

            this.Prestations.AddRange(
                new Prestation
                {
                    DateVoulue = new DateTime(2021, 10, 25),
                    Prix = 100,
                    ProviderId = 1,
                    ConsumerId = 1,
                    VoitureId = 1,
                    NumeroDevis = "18644537",
                    EtatPresta = Prestation.Etat.Valide

                },

                 new Prestation
                 {
                     DateVoulue = new DateTime(2021, 10, 30),
                     Prix = 300,
                     ProviderId = 1,
                     ConsumerId = 1,
                     VoitureId = 1,
                     NumeroDevis = "15975364",
                     EtatPresta = Prestation.Etat.En_cours
                 },

                 new Prestation
                 {
                     DateVoulue = new DateTime(2021, 10, 15),
                     Prix = 150,
                     ProviderId = 2,
                     ConsumerId = 3,
                     VoitureId = 3,
                     NumeroDevis = "51535957",
                     EtatPresta = Prestation.Etat.Valide
                 },

                 new Prestation
                 {
                     DateVoulue = new DateTime(2021, 10, 29),
                     Prix = 100,
                     ProviderId = 2,
                     ConsumerId = 2,
                     VoitureId = 4,
                     NumeroDevis = "84868785",
                     EtatPresta = Prestation.Etat.En_cours
                 }

                );

            this.SaveChanges();
        }
    }
}