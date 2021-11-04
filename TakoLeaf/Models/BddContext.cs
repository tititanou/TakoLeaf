using Microsoft.EntityFrameworkCore;
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
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<PostSignale> PostSignales { get; set; }
        public DbSet<Amitie> Amities { get; set; }
        public DbSet<DemandeDevisListeCompetence> DemandesDevisListeCompetence { get; set; }
        public DbSet<DemandeDevisListeRessource> DemandesDevisListeRessource { get; set; }
        public DbSet<MessageEnvoye> MessageEnvoyes { get; set; }
        public DbSet<MessageRecu> MessageRecus { get; set; }

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

            this.Adresses.AddRange(
                new Adresse
                {
                    Id = 1,
                    Rue = "14 Rue Bannier",
                    CodePostal = 45000,
                    Departement = 45,
                    Ville = "Orleans"
                },

                new Adresse
                {

                    Id = 2,
                    Rue = "148 rue du faubourg saint-martin",
                    CodePostal = 75010,
                    Departement = 75,
                    Ville = "Paris"
                },

                new Adresse
                {
                    Id = 3,
                    Rue = "74 Rue General de Gaulle",
                    CodePostal = 92250,
                    Departement = 92,
                    Ville = "La Garenne Colombes"
                },

                new Adresse
                {
                    Id = 4,
                    Rue = "14 Rue de la Victoire",
                    CodePostal = 57100,
                    Departement = 57,
                    Ville = "Thionville"
                },

                new Adresse
                {
                    Id = 5,
                    Rue = "1 Rue Boucher",
                    CodePostal = 30364,
                    Departement = 30,
                    Ville = "Guibert"
                },

                new Adresse
                {
                    Id = 6,
                    Rue = "15 Rue Pierre Nobel",
                    CodePostal = 45700,
                    Departement = 45,
                    Ville = "Villmandeur"

                },

                new Adresse
                {
                    Id = 7,
                    Rue = "13 Rue de Pottier",
                    CodePostal = 64127,
                    Departement = 64,
                    Ville = "Descamps"

                },

                new Adresse
                {
                    Id = 8,
                    Rue = "40 Rue des Coteaux",
                    CodePostal = 64127,
                    Departement = 64,
                    Ville = "Tonnay"

                },

                new Adresse
                {
                    Id = 9,
                    Rue = "13 Avenue de la république",
                    CodePostal = 64127,
                    Departement = 64,
                    Ville = "Lyon"

                },

                new Adresse
                {
                    Id = 10,
                    Rue = "25 Rue de la Feuille",
                    CodePostal = 85000,
                    Departement = 85,
                    Ville = "Konoha"

                }
                );

            this.Adherents.AddRange(
                new Adherent
                {
                    Id = 1,
                    Nom = "GIGOT",
                    Prenom = "Loic",
                    Date_naissance = new DateTime(1990, 10, 23),
                    AdresseId = 1,
                    Telephone = "0658423947"

                },

                 new Adherent
                 {
                     Id = 2,
                     Nom = "ROJAS",
                     Prenom = "Tania",
                     Date_naissance = new DateTime(1980, 02, 10),
                     AdresseId = 2,
                     Telephone = "0693562410"
                 },

                 new Adherent
                 {
                     Id = 3,
                     Nom = "ZAWARTOSKI",
                     Prenom = "Valentin",
                     Date_naissance = new DateTime(1994, 12, 01),
                     AdresseId = 3,
                     Telephone = "0652471230"
                 },

                new Adherent
                {
                    Id = 4,
                    Nom = "DAUPHIN",
                    Prenom = "Anthony",
                    Date_naissance = new DateTime(1993, 08, 10),
                    AdresseId = 4,
                    Telephone = "0626576356"
                },



                new Adherent
                {
                    Id = 5,
                    Nom = "Guillaume",
                    Prenom = "Levy",
                    Date_naissance = new DateTime(1990, 07, 18),
                    AdresseId = 5,
                    Telephone = "0618525231"

                },

                new Adherent
                {
                    Id = 6,
                    Nom = "Andre",
                    Prenom = "Soulard",
                    Date_naissance = new DateTime(1955, 12, 12),
                    AdresseId = 6,
                    Telephone = "0699885544"

                },

                new Adherent
                {
                    Id = 7,
                    Nom = "Haroun",
                    Prenom = "Humo",
                    Date_naissance = new DateTime(1990, 07, 05),
                    AdresseId = 7,
                    Telephone = "0699885544"
                }

                  /*new Adherent
                  {
                      Id = 8,
                      Nom = "Jean",
                      Prenom = "Neymar",
                      Date_naissance = new DateTime(1967, 10, 09),
                      AdresseId = 8,
                      Telephone = "0699885544"

                  },

                  new Adherent
                  {
                      Id = 9,
                      Nom = "Prunelle",
                      Prenom = "Demaizieux",
                      Date_naissance = new DateTime(1980, 04, 22),
                      AdresseId = 9,
                      Telephone = "0699885544"


                  }*/

                );

            this.Historiques.AddRange(

                new Historique { Id = 1, AdherentId = 1 },
                new Historique { Id = 2, AdherentId = 2 },
                new Historique { Id = 3, AdherentId = 3 },
                new Historique { Id = 4, AdherentId = 4 },
                new Historique { Id = 5, AdherentId = 5 },
                new Historique { Id = 6, AdherentId = 6 },
                new Historique { Id = 7, AdherentId = 7 }
                //new Historique { Id = 8, AdherentId = 8 },
                //new Historique { Id = 9, AdherentId = 9 }

                );

            this.CompteUsers.AddRange(
                 new CompteUser
                 {
                     Mail = "al@gmail.com",
                     MotDePasse = dal.EncodeMD5("123"),
                     Description = "Aime les fleurs et les voitures de collection",
                     EtatProfil = EtatProfil.VALIDE,
                     AdherentId = 1,
                     Avatar ="5561859.jpg",
                     Role = "Provider"

                 },

                  new CompteUser
                  {
                      Mail = "Rojas.Tania@gmail.com",
                      MotDePasse = dal.EncodeMD5("LaMama"),
                      Description = "Passionée de majorette",
                      EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                      AdherentId = 2,
                      Role = "Consumer"

                  },
                  new CompteUser
                  {
                      Mail = "Zawartoski.Valentin@gmail.com",
                      MotDePasse = dal.EncodeMD5("ZeFrontiste"),
                      Description = "Passioné de moto",
                      EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                      AdherentId = 3,
                      Role = "Consumer"
                  },
                   new CompteUser
                   {
                       Mail = "Anthony.Dauphin@gmail.com",
                       MotDePasse = dal.EncodeMD5("ElBrutos"),
                       Description = "Veille au grain",
                       EtatProfil = EtatProfil.VALIDE,
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
                       Description = "Bonjour a tous je suis Andre Soulard et je ne suis pas un fleche en informatique mais ce site simple m'a rasuré. Je fais maintenant parti de la famille TakoLeaf et ai hate d'echanger avec vous sur le domaine de l'automodbile",
                       EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                       AdherentId = 6,
                       Role = "Consumer",
                       Avatar = "soulard.jpg"
                   },

                   new CompteUser
                   {
                       Mail = "haroun@gmail.com",
                       MotDePasse = dal.EncodeMD5("123"),
                       Description = "Je suis drole",
                       EtatProfil = EtatProfil.VALIDE,
                       AdherentId = 7,
                       Role = "Provider"
                   }


                   );

            this.Amities.Add(
                new Amitie
                {
                    Id = 1,
                    AdherentAmiId = 3,
                    AdherentCourantId = 6
                }

                );

            this.Consumers.AddRange(

                new Consumer
                {
                    Id = 1,
                    AdherentId = 2
                },

                new Consumer
                {
                    Id = 2,
                    AdherentId = 3
                },

                new Consumer
                {
                    Id = 3,
                    AdherentId = 6
                }
                );

            this.Providers.AddRange(
                new Provider
                {
                    Id = 1,
                    Note = 4.5,
                    RibId = 1,
                    AdherentId = 1,
                    Rang = Rang.MAITRE_KRAKEN
                },

                new Provider
                {
                    Id = 2,
                    Note = 0,
                    RibId = 2,
                    AdherentId = 5,
                    Rang = Rang.POULPE_AMATEUR
                },

                new Provider
                {
                    Id = 3,
                    Note = 0,
                    RibId = 3,
                    AdherentId = 7,
                    Rang = Rang.POULPE_BRICOLEUR
                }
                );



            this.Cartes.AddRange(
                new Carte
                {
                    Id = 1,
                    Titulaire = "MME ROJAS",
                    NumeroCarte = "1578323524729856",
                    ExpirDate = "08/2023",
                    Crypto = 458,
                    ConsumerId = 1

                },

                new Carte
                {
                    Id = 2,
                    Titulaire = "MR ZAWARTOSKI",
                    NumeroCarte = "1234567891012023",
                    ExpirDate = "10/2023",
                    Crypto = 246,
                    ConsumerId = 2
                },

                new Carte
                {
                    Id = 3,
                    Titulaire = "MR SOULARD",
                    NumeroCarte = "1415242536359695",
                    ExpirDate = "01/2024",
                    Crypto = 666,
                    ConsumerId = 3
                }
                 
                );



            this.Ribs.AddRange(
                 new Rib
                 {
                     Id = 1,
                     Titulaire = "Alexis Abrate",
                     Iban = "1245 1245 7777 1245",
                     Banque = "Boursorama"

                 },

                new Rib
                {
                    Id = 2,
                    Titulaire = "Guillaume Levy",
                    Iban = "1245 1245 1245 1245",
                    Banque = "Boursorama"

                },

                new Rib
                {
                    Id = 3,
                    Titulaire = "Haroun",
                    Iban = "1245 1245 7722 1245",
                    Banque = "Boursorama"

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
                    Titulaire = "MME TANIA ROJAS",
                    ModeleId = 2

                },

                 new Voiture
                 {
                     Id = 5,
                     Annee = 2018,
                     Carburant = Carburant.DIESEL,
                     ConsumerId = 1,
                     Immatriculation = "SE-185-BB",
                     Titulaire = "MME TANIA ROJAS",
                     ModeleId = 3

                 },

                  new Voiture
                  {
                      Id = 6,
                      Annee = 2015,
                      Carburant = Carburant.DIESEL,
                      ConsumerId = 1,
                      Immatriculation = "DD-455-FR",
                      Titulaire = "MME TANIA ROJAS",
                      ModeleId = 6

                  },


                new Voiture
                {
                    Id = 2,
                    Annee = 2017,
                    Carburant = Carburant.SP98,
                    ConsumerId = 2,
                    Immatriculation = "BB-585-DF",
                    Titulaire = "MR VALENTIN ZAWARTOSKI",
                    ModeleId = 5
                },

                  new Voiture
                  {
                      Id = 7,
                      Annee = 2010,
                      Carburant = Carburant.DIESEL,
                      ConsumerId = 2,
                      Immatriculation = "CE-255-SE",
                      Titulaire = "MR VALENTIN ZAWARTOSKI",
                      ModeleId = 7
                  },

                  new Voiture
                  {
                        Id = 8,
                        Annee = 2007,
                        Carburant = Carburant.SP98,
                        ConsumerId = 2,
                        Immatriculation = "QZ-111-DF",
                        Titulaire = "MR VALENTIN ZAWARTOSKI",
                        ModeleId = 9
                  },

                new Voiture
                {
                    Id = 3,
                    Annee = 2015,
                    Carburant = Carburant.DIESEL,
                    ConsumerId = 3,
                    Immatriculation = "BD-524-AV",
                    Titulaire = "MR SOULARD",
                    ModeleId = 5
                },

                new Voiture
                {
                    Id = 9,
                    Annee = 2020,
                    Carburant = Carburant.DIESEL,
                    ConsumerId = 3,
                    Immatriculation = "CC-174-GF",
                    Titulaire = "MR SOULARD",
                    ModeleId = 15
                },

                new Voiture
                {
                    Id = 10,
                    Annee = 2018,
                    Carburant = Carburant.DIESEL,
                    ConsumerId = 3,
                    Immatriculation = "CC-484-CD",
                    Titulaire = "MME SOULARD",
                    ModeleId = 17
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
                    CorpsPost = "Salut, jai un pb avec Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec faucibus efficitur fermentum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec volutpat nunc quis tellus lacinia.",
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
                new Marque { Id = 1, Nom = "Volkswagen" },
                new Marque { Id = 2, Nom = "Peugeot" },
                new Marque { Id = 3, Nom = "Audi" },
                new Marque { Id = 4, Nom = "Nissan" },
                new Marque { Id = 5, Nom = "Citroen" },
                new Marque { Id = 6, Nom = "Alpha Romeo" },
                new Marque { Id = 7, Nom = "BMW" },
                new Marque { Id = 8, Nom = "Chevrolet" },
                new Marque { Id = 9, Nom = "Dacia" },
                new Marque { Id = 10, Nom = "Fiat" },
                new Marque { Id = 11, Nom = "KIA" },
                new Marque { Id = 12, Nom = "Opel" },
                new Marque { Id = 13, Nom = "Renault" },
                new Marque { Id = 14, Nom = "Skoda" },
                new Marque { Id = 15, Nom = "Toyota" },
                new Marque { Id = 16, Nom = "Volvo" },
                new Marque { Id = 17, Nom = "Mercedes" }
                );

            this.Modeles.AddRange(
                new Modele { Nom = "Polo", MarqueId = 1 },
                new Modele { Nom = "Golf", MarqueId = 1 },
                new Modele { Nom = "Passat", MarqueId = 1 },
                new Modele { Nom = "Tiguan", MarqueId = 1 },
                new Modele { Nom = "Touran", MarqueId = 1 },
                new Modele { Nom = "T-Roc", MarqueId = 1 },
                new Modele { Nom = "Touareg", MarqueId = 1 },

                new Modele { Nom = "106", MarqueId = 2 },
                new Modele { Nom = "108", MarqueId = 2 },
                new Modele { Nom = "206", MarqueId = 2 },
                new Modele { Nom = "206", MarqueId = 2 },
                new Modele { Nom = "207", MarqueId = 2 },
                new Modele { Nom = "208", MarqueId = 2 },
                new Modele { Nom = "306", MarqueId = 2 },
                new Modele { Nom = "307", MarqueId = 2 },
                new Modele { Nom = "308", MarqueId = 2 },
                new Modele { Nom = "1007", MarqueId = 2 },
                new Modele { Nom = "1008", MarqueId = 2 },
                new Modele { Nom = "2008", MarqueId = 2 },
                new Modele { Nom = "3008", MarqueId = 2 },
                new Modele { Nom = "5008", MarqueId = 2 },
               
                new Modele { Nom = "A1", MarqueId = 3 },
                new Modele { Nom = "A2", MarqueId = 3 },
                new Modele { Nom = "A3", MarqueId = 3 },
                new Modele { Nom = "A4", MarqueId = 3 },
                new Modele { Nom = "A5", MarqueId = 3 },
                new Modele { Nom = "TT", MarqueId = 3 },
             

                new Modele { Nom = "Qashquai", MarqueId = 4 },

                new Modele { Nom = "Picasso", MarqueId = 5 },
                new Modele { Nom = "C4", MarqueId = 5 },

                new Modele { Nom = "Ibiza", MarqueId = 6 },
                new Modele { Nom = "Leon", MarqueId = 6 }
                );

            this.CateCompetences.AddRange(
                new CateCompetence { Id = 1, Intitule = "Batterie" },
                new CateCompetence { Id = 2, Intitule = "Carrosserie" },
                new CateCompetence { Id = 3, Intitule = "Climatisation & chauffage" },
                new CateCompetence { Id = 4, Intitule = "Diagnostic" },
                new CateCompetence { Id = 5, Intitule = "Direction" },
                new CateCompetence { Id = 6, Intitule = "Echappement" },
                new CateCompetence { Id = 7, Intitule = "Embrayage" },
                new CateCompetence { Id = 8, Intitule = "Freinage" },
                new CateCompetence { Id = 9, Intitule = "Moteur" },
                new CateCompetence { Id = 10, Intitule = "Pneu" }

                );

            this.SsCateCompetences.AddRange(
                new SsCateCompetence { Id=1, Intitule = "Remplacement alternateur", CateCompetenceId = 1 },
                new SsCateCompetence { Id = 2, Intitule = "Remplacement demarreur", CateCompetenceId = 1 },
                new SsCateCompetence { Id = 3, Intitule = "Remplacement de la batterie", CateCompetenceId = 1 },

                new SsCateCompetence { Id = 4, Intitule = "Changement d'aile", CateCompetenceId = 2 },
                new SsCateCompetence { Id = 5, Intitule = "Reparation pare choc", CateCompetenceId = 2 },

                new SsCateCompetence { Id = 6, Intitule = "Entretien de la climatisation", CateCompetenceId = 3 },
                new SsCateCompetence { Id = 7, Intitule = "Probleme de chauffage", CateCompetenceId = 3 },
                new SsCateCompetence { Id = 8, Intitule = "Entretien et recharge de clim", CateCompetenceId = 3 },
                new SsCateCompetence { Id = 9, Intitule = "Remplacement du filtre habitacle", CateCompetenceId = 3 },

                new SsCateCompetence { Id = 10, Intitule = "Diagnostique electronique auto", CateCompetenceId = 4 },

                new SsCateCompetence { Id = 11, Intitule = "Remplacement des biellettes", CateCompetenceId = 5 },
                new SsCateCompetence { Id = 12, Intitule = "Remplacement de cadran", CateCompetenceId = 5 },
                new SsCateCompetence { Id = 13, Intitule = "Remplacement des rotules directions", CateCompetenceId = 5 },

                new SsCateCompetence { Id = 14, Intitule = "Remplacement de catalyseur", CateCompetenceId = 6 },
                new SsCateCompetence { Id = 15, Intitule = "Remplacement de vanne EGR", CateCompetenceId = 6 },
                new SsCateCompetence { Id = 16, Intitule = "Remplacement du filtre a particules", CateCompetenceId = 6 },
                new SsCateCompetence { Id = 17, Intitule = "Remplacement Silencieux arriere", CateCompetenceId = 6 },

                new SsCateCompetence { Id = 18, Intitule = "Remplacement Kit Embrayage", CateCompetenceId = 7 },
                new SsCateCompetence { Id = 19, Intitule = "Probleme embrayage", CateCompetenceId = 7 },
                new SsCateCompetence { Id = 20, Intitule = "Remplacement volant moteur", CateCompetenceId = 7 },

                new SsCateCompetence { Id = 21, Intitule = "Remplacement de cable", CateCompetenceId = 8 },
                new SsCateCompetence { Id = 22, Intitule = "Remplacement du liquide de frein", CateCompetenceId = 8 },


                new SsCateCompetence { Id = 23, Intitule = "Remplacement des injecteurs", CateCompetenceId = 9 },
                new SsCateCompetence { Id = 24, Intitule = "Remplacement filtre à air", CateCompetenceId = 9 },
                new SsCateCompetence { Id = 25, Intitule = "Décalaminage moteur", CateCompetenceId = 9 },
                new SsCateCompetence { Id = 26, Intitule = "Probleme d’allumage du moteur", CateCompetenceId = 9 },
                new SsCateCompetence { Id = 27, Intitule = "Remplacement des bougies", CateCompetenceId = 9 },
                new SsCateCompetence { Id = 28, Intitule = "Remplacement liquide de refroidissement", CateCompetenceId = 9 },

                new SsCateCompetence { Id = 29, Intitule = "Equilibrage", CateCompetenceId = 10 },
                new SsCateCompetence { Id = 30, Intitule = "Changer Roue", CateCompetenceId = 10 },
                new SsCateCompetence { Id = 31, Intitule = "Changer Pneu", CateCompetenceId = 10 }


                );
            this.Competences.AddRange(
                new Competence { Id = 1, ProviderId = 1, SsCateCompetenceId = 6, NomSsCate = "Changer Roue", TarifHoraire = 40 },
                new Competence { Id = 2, ProviderId = 1, SsCateCompetenceId = 3, NomSsCate = "Remplacement de la batterie", TarifHoraire = 50 },
                new Competence { Id = 3, ProviderId = 1, SsCateCompetenceId = 2, NomSsCate = "Remplacement demarreur", TarifHoraire = 17 },
                new Competence { Id = 4, ProviderId = 1, SsCateCompetenceId = 14, NomSsCate = "Remplacement de catalyseur", TarifHoraire = 44 },

                new Competence { Id = 5, ProviderId = 2, SsCateCompetenceId = 1, NomSsCate = "Remplacement alternateur", TarifHoraire = 42 },
                new Competence { Id = 6, ProviderId = 2, SsCateCompetenceId = 3, NomSsCate = "Remplacement de la batterie", TarifHoraire = 46 },
                new Competence { Id = 7, ProviderId = 2, SsCateCompetenceId = 9, NomSsCate = "Remplacement du filtre habitaclen", TarifHoraire = 48 },
                new Competence { Id = 8, ProviderId = 2, SsCateCompetenceId = 6, NomSsCate = "Changer Roue", TarifHoraire = 31 },
                new Competence { Id = 9, ProviderId = 2, SsCateCompetenceId = 15, NomSsCate = "Remplacement de vanne EGR", TarifHoraire = 58 },
                new Competence { Id = 10, ProviderId = 2, SsCateCompetenceId = 20, NomSsCate = "Remplacement volant moteur", TarifHoraire = 14 },

                new Competence { Id = 11, ProviderId = 3, SsCateCompetenceId = 2, NomSsCate = "Changer la distribution", TarifHoraire = 19 },
                new Competence { Id = 12, ProviderId = 3, SsCateCompetenceId = 6, NomSsCate = "Changer Roue", TarifHoraire = 32 },
                new Competence { Id = 13, ProviderId = 3, SsCateCompetenceId = 1, NomSsCate = "Remplacement alternateur", TarifHoraire = 40 },
                new Competence { Id = 14, ProviderId = 3, SsCateCompetenceId = 14, NomSsCate = "Remplacement de catalyseur", TarifHoraire = 31 },
                new Competence { Id = 15, ProviderId = 3, SsCateCompetenceId = 16, NomSsCate = "Remplacement du filtre a particules", TarifHoraire = 67 },
                new Competence { Id = 16, ProviderId = 3, SsCateCompetenceId = 22, NomSsCate = "Remplacement des bougies", TarifHoraire = 55 }

                );

            this.Ressources.AddRange(

                new Ressource
                {
                    Intitule = "Cle à molette",
                    AdresseId = 1,
                    Categorie = CateRessource.OUTIL,
                    Disponible = true,
                    TarifJournalier = 15,
                    ProviderId = 1
                },

                  new Ressource
                  {
                      Intitule = "Vidangeuse",
                      AdresseId = 5,
                      Categorie = CateRessource.OUTIL_SPECIALISE,
                      Disponible = true,
                      TarifJournalier = 40,
                      ProviderId = 1
                  },

                  new Ressource

                  {
                      Intitule = "Garage TakoLef",
                      AdresseId = 10,
                      Categorie = CateRessource.LOCAL_GARAGE,
                      Disponible = true,
                      TarifJournalier = 100,
                      ProviderId = 2
                  },

                  new Ressource
                  {
                      Intitule = "Remorque 3m^3",
                      AdresseId = 5,
                      Categorie = CateRessource.REMORQUE,
                      Disponible = true,
                      TarifJournalier = 55,
                      ProviderId = 2
                  }




                );





            this.Articles.AddRange(
                new Article
                {
                    AdminId = "Admin",
                    Titre = "Lancement de TakoLeaf!",
                    DateRedaction = new DateTime(2021, 10, 19),
                    DatePublication = new DateTime(2021, 10, 24),
                    Texte = "Soleo saepe ante oculos ponere, idque libenter crebris usurpare sermonibus, omnis nostrorum imperatorum, omnis exterarum gentium potentissimorumque populorum, omnis clarissimorum regum res gestas, cum tuis nec contentionum magnitudine nec numero proeliorum nec varietate regionum nec celeritate conficiendi nec dissimilitudine bellorum posse conferri; nec vero disiunctissimas terras citius passibus cuiusquam potuisse peragrari, quam tuis non dicam cursibus, sed victoriis lustratae sunt.",
                    Public = true
                },

                 new Article
                 {
                     AdminId = "Admin",
                     Titre = "Flash Spécial !",
                     DateRedaction = new DateTime(2021, 10, 22),
                     DatePublication = new DateTime(2021, 10, 23),
                     Texte = "Intellectum est enim mihi quidem in multis, et maxime in me ipso, sed paulo ante in omnibus, cum M. Marcellum senatui reique publicae concessisti, commemoratis praesertim offensionibus, te auctoritatem huius ordinis dignitatemque rei publicae tuis vel doloribus vel suspicionibus anteferre. Ille quidem fructum omnis ante actae vitae hodierno die maximum cepit, cum summo consensu senatus, tum iudicio tuo gravissimo et maximo. Ex quo profecto intellegis quanta in dato beneficio sit laus, cum in accepto sit tanta gloria.",
                     Public = true
                 },

                  new Article
                  {
                      AdminId = "Admin",
                      Titre = "Attention les poulpes, il y a du nouveau !",
                      DateRedaction = new DateTime(2021, 10, 20),
                      DatePublication = new DateTime(2021, 10, 21),
                      Texte = "Intellectum est enim mihi quidem in multis, et maxime in me ipso, sed paulo ante in omnibus, cum M. Marcellum senatui reique publicae concessisti, commemoratis praesertim offensionibus, te auctoritatem huius ordinis dignitatemque rei publicae tuis vel doloribus vel suspicionibus anteferre. Ille quidem fructum omnis ante actae vitae hodierno die maximum cepit, cum summo consensu senatus, tum iudicio tuo gravissimo et maximo. Ex quo profecto intellegis quanta in dato beneficio sit laus, cum in accepto sit tanta gloria.",
                      Public = false
                  }
                );

           
            //this.Devis.AddRange(

            //    //new Devis
            //    //{
            //    //    NumeroDevis="azerty12345",
            //    //    DateEmission= new DateTime(2021,10,15),
            //    //    Tarif=150,
            //    //    DateDebut= new DateTime(2021,11,10),
            //    //    DateFin= new DateTime(2021,11,16),
            //    //    DescriptionPresta = " blablablablabla",
            //    //    LieuPresta="Paris",
            //    //    EtatDevis= EtatDevis.EN_ATTENTE,
            //    //    ProviderId = 1,
            //    //    ConsumerId = 2,
            //    //    VoitureId = 1,                   

            //    //}

            //    );

            this.Messages.AddRange(
                new Message
                {
                    Id = 1,
                    AdherentExpId = 5,
                    AdherentDestId = 3,
                    Titre = "Info",
                    Msg = "Turpis augue lorem justo consequat litora. Nullam habitant ullamcorper aliquam. Conubia tempor sem turpis platea mi pulvinar. At leo turpis dui. Morbi lacus habitant inceptos accumsan aliquam dui quisque.",
                    Date = new DateTime(2021, 09, 12, 23, 41, 16),
                    Lu = true
                },
                 new Message
                 {
                     Id = 2,
                     AdherentExpId = 3,
                     AdherentDestId = 5,
                     Titre = "Reçu",
                     Msg = "Lacinia felis integer interdum. Praesent neque arcu dictumst enim cubilia rhoncus laoreet. Et tristique ullamcorper auctor metus etiam nibh augue sociosqu.",
                     Date = new DateTime(2021, 09, 13, 07, 34, 22),
                     Lu = false
                 },
                  new Message
                  {
                      Id = 3,
                      AdherentExpId = 5,
                      AdherentDestId = 1,
                      Titre = "Blabla",
                      Msg = "Interdum sociosqu porta sagittis aenean mauris.",
                      Date = new DateTime(2021, 09, 14, 09, 12, 06),
                      Lu = false
                  },
                   new Message
                   {
                       Id = 4,
                       AdherentExpId = 2,
                       AdherentDestId = 5,
                       Titre = "Okay",
                       Msg = "Aliquet quisque nam a sociosqu dictumst quisque pharetra a. Varius venenatis iaculis tortor.",
                       Date = new DateTime(2021, 10, 01, 05, 16, 33),
                       Lu = false
                   },
                    new Message
                    {
                        Id = 5,
                        AdherentExpId = 5,
                        AdherentDestId = 2,
                        Titre = "Alors",
                        Msg = "Lectus praesent eleifend in. Adipiscing fames congue. Scelerisque lectus ad habitant lacus sapien.",
                        Date = new DateTime(2021, 11, 15, 12, 36, 57),
                        Lu = false
                    }
                );
            this.MessageEnvoyes.AddRange(
                new MessageEnvoye
                {
                    Id = 1,
                    MessageId = 1,
                    ExpediteurId = 5,
                    DestinataireId = 3
                },
                new MessageEnvoye
                {
                    Id = 2,
                    MessageId = 2,
                    ExpediteurId = 3,
                    DestinataireId = 5
                },
                new MessageEnvoye
                {
                    Id = 3,
                    MessageId = 3,
                    ExpediteurId = 5,
                    DestinataireId = 1
                },
                new MessageEnvoye
                {
                    Id = 4,
                    MessageId = 4,
                    ExpediteurId = 2,
                    DestinataireId = 5
                },
                new MessageEnvoye
                {
                    Id = 5,
                    MessageId = 5,
                    ExpediteurId = 5,
                    DestinataireId = 2
                });

            this.PostSignales.Add(
            new PostSignale
            {
                Id = 1,
                Date = DateTime.Now.ToShortDateString(),
                Vu = false,
                Message = "Je souhaite signaler ce compte car il me semble inapproprié",
                AdherentSignaleId = 1,
                AdherentSignalantId =2,
                PostId = 1
            }
            );


            


           

            this.MessageRecus.AddRange(
                new MessageRecu
                {
                    Id = 1,
                    MessageId = 1,
                    ExpediteurId = 5,
                    DestinataireId = 3
                },
                new MessageRecu
                {
                    Id = 2,
                    MessageId = 2,
                    ExpediteurId = 3,
                    DestinataireId = 5
                },
                new MessageRecu
                {
                    Id = 3,
                    MessageId = 3,
                    ExpediteurId = 5,
                    DestinataireId = 1
                },
                new MessageRecu
                {
                    Id = 4,
                    MessageId = 4,
                    ExpediteurId = 2,
                    DestinataireId = 5
                },
                new MessageRecu
                {
                    Id = 5,
                    MessageId = 5,
                    ExpediteurId = 5,
                    DestinataireId = 2
                });




            this.DemandeDevis.AddRange(
                new DemandeDevis
                {
                    Id = 1,
                    DateDemande = new DateTime(2021, 10, 31),
                    DateDebutVoulue = new DateTime(2021, 11, 7),
                    Message = "Bonjour je suis la demande 1 Provider 1",
                    ConsumerId = 3,
                    ProviderId = 1,
                    VoitureId = 3
                },

                 new DemandeDevis
                 {
                     Id = 2,
                     DateDemande = new DateTime(2021, 08, 18),
                     DateDebutVoulue = new DateTime(2021, 08, 24),
                     Message = "Bonjour je suis la demande 1 Provider 2",
                     ConsumerId = 3,
                     ProviderId = 2,
                     VoitureId = 9
                 },


                new DemandeDevis
                {
                    Id = 3,
                    DateDemande = new DateTime(2021, 10, 31),
                    DateDebutVoulue = new DateTime(2021, 11, 19),
                    Message = "Bonjour je suis la demande 2 Provider 1",
                    ConsumerId = 1,
                    ProviderId = 1,
                    VoitureId = 5
                }



                );

            this.DemandesDevisListeCompetence.AddRange(

               new DemandeDevisListeCompetence
               {
                   Id = 1,
                   CompetenceId = 1,
                   DemandeDevisId = 1

               },

               new DemandeDevisListeCompetence
               {
                   Id = 2,
                   CompetenceId = 4,
                   DemandeDevisId = 1

               },

                new DemandeDevisListeCompetence
                {
                    Id = 3,
                    CompetenceId = 2,
                    DemandeDevisId = 1

                },

                new DemandeDevisListeCompetence
                {
                    Id = 4,
                    CompetenceId = 6,
                    DemandeDevisId = 2

                },

                new DemandeDevisListeCompetence
                {
                    Id = 5,
                    CompetenceId = 5,
                    DemandeDevisId = 2

                },

               new DemandeDevisListeCompetence
               {
                   Id = 6,
                   CompetenceId = 1,
                   DemandeDevisId = 3

               }

               );

            this.DemandesDevisListeRessource.AddRange(
                new DemandeDevisListeRessource
                {
                    Id = 1,
                    RessourceId = 4,
                    DemandeDevisId = 1

                },

                new DemandeDevisListeRessource
                {
                    Id = 2,
                    RessourceId = 4,
                    DemandeDevisId = 2

                }

                );


            this.Prestations.AddRange(
               new Prestation
               {
                   Id=1,
                   DateDebut = new DateTime(2020, 07, 25),
                   Prix = 100,
                   ProviderId = 1,
                   ConsumerId = 3,
                   VoitureId = 5,
                   NumeroDevis = "18644537",
                   EtatPresta = Prestation.Etat.Valide

               },

                new Prestation
                {
                    Id=2,
                    DateDebut = new DateTime(2021, 10, 30),
                    Prix = 300,
                    ProviderId = 1,
                    ConsumerId = 3,
                    VoitureId = 9,
                    NumeroDevis = "15975364",
                    EtatPresta = Prestation.Etat.Valide
                },

                new Prestation
                {
                    Id=3,
                    DateDebut = new DateTime(2021, 10, 15),
                    Prix = 150,
                    ProviderId = 2,
                    ConsumerId = 3,
                    VoitureId = 3,
                    NumeroDevis = "51535957",
                    EtatPresta = Prestation.Etat.Valide
                },

                new Prestation
                {
                    Id=4,
                    DateDebut = new DateTime(2021, 10, 29),
                    Prix = 100,
                    ProviderId = 2,
                    ConsumerId = 2,
                    VoitureId = 4,
                    NumeroDevis = "84868785",
                    EtatPresta = Prestation.Etat.En_cours
                }

               );

            this.HistoriquePrestas.AddRange(
                new HistoriquePresta
                {
                    Id = 1,
                    HistoriqueId = 6,
                    PrestationId = 1
                },

                new HistoriquePresta
                {
                    Id = 2,
                    HistoriqueId = 6,
                    PrestationId = 2
                },
                new HistoriquePresta
                {
                    Id = 3,
                    HistoriqueId = 6,
                    PrestationId = 3
                },

                 new HistoriquePresta
                 {
                     Id = 4,
                     HistoriqueId = 1,
                     PrestationId = 1
                 },

                new HistoriquePresta
                {
                    Id = 5,
                    HistoriqueId = 1,
                    PrestationId = 2
                },
                new HistoriquePresta
                {
                    Id = 6,
                    HistoriqueId = 5,
                    PrestationId = 3
                }
                );

            this.Avis.AddRange(


                new Avis
                {
                    Id = 1,
                    ConsumerId = 3,
                    PrestationId = 1,
                    ProviderId = 1,
                    DateCreation = new DateTime(2020, 07, 28),
                    Note = 4,
                    Contenu = "Très bon boulot et personne très agréable"

                },

                new Avis
                {
                    Id = 2,
                    ConsumerId = 3,
                    PrestationId = 2,
                    ProviderId = 1,
                    DateCreation = new DateTime(2020, 11, 01),
                    Note = 5,
                    Contenu = "De nouveau du très bon boulot et tres efficace"

                },

                new Avis
                {
                    Id = 3,
                    ConsumerId = 3,
                    PrestationId = 3,
                    ProviderId = 2,
                    DateCreation = new DateTime(2020, 10, 17),
                    Note = 3,
                    Contenu = "Bon boulot et mais pas très agréable"

                }

                );

            this.SaveChanges();
        }
    }
}