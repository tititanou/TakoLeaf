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
                    Rue = "124 Rue Hoche",
                    CodePostal = 93100,
                    Ville = "Montreuil"
                },

                new Adresse
                {
                  
                     Id = 2,
                     Rue = "205 Rue de Perrin",
                     CodePostal = 43142,
                     Ville = "Hoareau sur Mer"
                },

                new Adresse
                {
                      Id = 3,
                      Rue = "74 Rue General de Gaulle",
                      CodePostal = 92250,
                      Ville = "La Garenne Colombes"
                },

                new Adresse
                {
                       Id = 4,
                       Rue = "14 Rue de la Victoire",
                       CodePostal = 57100,
                       Ville = "Thionville"
                },

                new Adresse
                {
                        Id = 5,
                        Rue = "1 Rue Boucher",
                        CodePostal = 30364,
                        Ville = "Guibert"
                },

                new Adresse
                {
                    Id = 6,
                    Rue = "5 Chemin Susanne Joseph",
                    CodePostal = 13177,
                    Ville = "Leroux"

                },

                new Adresse
                {
                     Id = 7,
                     Rue = "13 Rue de Pottier",
                     CodePostal = 64127,
                     Ville = "Descamps"

                }
                );

            this.Adherents.AddRange(
                new Adherent
                {
                    Id = 1,
                    Nom = "ABRATE",
                    Prenom = "Alexis",
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
                    Telephone = "0618525231",

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
                );

            this.CompteUsers.AddRange(
                 new CompteUser
                 {
                     Mail = "Alexis.Abrate@gmail.com",
                     MotDePasse = dal.EncodeMD5("LeTruantDuCSharp"),
                     Description = "Hello",
                     EtatProfil = EtatProfil.VALIDE,
                     AdherentId = 1,
                     Role = "Provider"

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
                       EtatProfil = EtatProfil.NON_VALIDE,
                       AdherentId = 6,
                       Role = "Consumer"
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
                    Note = 0,
                    RibId = 1,
                    AdherentId = 1
                },

                new Provider
                {
                    Id = 2,
                    Note = 0,
                    RibId = 2,
                    AdherentId = 5
                },

                new Provider
                {
                    Id = 3,
                    Note = 0,
                    RibId = 3,
                    AdherentId = 7
                }
                );


               
            this.Cartes.AddRange(
                new Carte
                {
                    Id=1,
                    Titulaire = "MME ROJAS",
                    NumeroCarte = "1578323524729856",
                    ExpirDate = "08/2023",
                    Crypto = 458,
                    ConsumerId = 1
                    
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
                new Marque { Nom = "Volkswagen" },
                new Marque { Nom = "Peugeot" },
                new Marque { Nom = "Audi"},
                new Marque { Nom = "Nissan"},
                new Marque { Nom = "Citroen"},
                new Marque { Nom = "Seat"}
                );

            this.Modeles.AddRange(
                new Modele { Nom = "Polo", MarqueId = 1 },
                new Modele { Nom = "Golf", MarqueId = 1 },
                new Modele { Nom = "206", MarqueId = 2 },
                new Modele { Nom = "607", MarqueId = 2 },
                new Modele { Nom = "A2", MarqueId = 3 },
                new Modele { Nom = "A4", MarqueId = 3},
                new Modele { Nom = "Qashquai", MarqueId = 4},
                new Modele { Nom = "Picasso", MarqueId = 5},
                new Modele { Nom = "C4", MarqueId = 5 },
                new Modele { Nom = "Ibiza", MarqueId = 6},
                new Modele { Nom = "Leon", MarqueId =6}
                );

            this.CateCompetences.AddRange(
                new CateCompetence { Intitule = "Batterie" },
                new CateCompetence { Intitule = "Carrosserie" },
                new CateCompetence { Intitule = "Climatisation & chauffage" },
                new CateCompetence { Intitule = "Diagnostic" },
                new CateCompetence { Intitule = "Direction" },
                new CateCompetence { Intitule = "Echappement" },
                new CateCompetence { Intitule = "Embrayage" },
                new CateCompetence { Intitule = "Freinage" },
                new CateCompetence { Intitule = "Moteur" },
                new CateCompetence { Intitule = "Pneu" }              
                
                );

            this.SsCateCompetences.AddRange(
                new SsCateCompetence { Intitule ="Remplacement alternateur", CateCompetenceId = 1 },
                new SsCateCompetence { Intitule = "Remplacement demarreur", CateCompetenceId = 1 },
                new SsCateCompetence { Intitule = "Remplacement de la batterie", CateCompetenceId = 1 },

                new SsCateCompetence { Intitule = "Changement d'aile", CateCompetenceId = 2 },
                new SsCateCompetence { Intitule = "Reparation pare choc", CateCompetenceId = 2 },

                new SsCateCompetence { Intitule = "Entretien de la climatisation", CateCompetenceId = 3 },
                new SsCateCompetence { Intitule = "Probleme de chauffage", CateCompetenceId = 3 },
                new SsCateCompetence { Intitule = "Entretien et recharge de clim", CateCompetenceId = 3 },
                new SsCateCompetence { Intitule = "Remplacement du filtre habitacle", CateCompetenceId = 3 },

                new SsCateCompetence { Intitule = "Diagnostique electronique auto", CateCompetenceId = 4 },

                new SsCateCompetence { Intitule = "Remplacement des biellettes", CateCompetenceId = 5 },
                new SsCateCompetence { Intitule = "Remplacement de cadran", CateCompetenceId = 5 },
                new SsCateCompetence { Intitule = "Remplacement des rotules directions", CateCompetenceId = 5 },

                new SsCateCompetence { Intitule = "Remplacement de catalyseur", CateCompetenceId = 6 },
                new SsCateCompetence { Intitule = "Remplacement de vanne EGR", CateCompetenceId = 6 },
                new SsCateCompetence { Intitule = "Remplacement du filtre a particules", CateCompetenceId = 6 },
                new SsCateCompetence { Intitule = "Remplacement Silencieux arriere", CateCompetenceId = 6 },

                new SsCateCompetence { Intitule = "Remplacement Kit Embrayage", CateCompetenceId = 7 },
                new SsCateCompetence { Intitule = "Probleme embrayage", CateCompetenceId = 7 },
                new SsCateCompetence { Intitule = "Remplacement volant moteur", CateCompetenceId = 7 },

                new SsCateCompetence { Intitule = "Remplacement de cable", CateCompetenceId = 8 },
                new SsCateCompetence { Intitule = "Remplacement du liquide de frein", CateCompetenceId = 8},


                new SsCateCompetence { Intitule = "Remplacement des injecteurs", CateCompetenceId = 9 },
                new SsCateCompetence { Intitule = "Remplacement filtre à air", CateCompetenceId = 9 },
                new SsCateCompetence { Intitule = "Décalaminage moteur", CateCompetenceId = 9 },
                new SsCateCompetence { Intitule = "Probleme d’allumage du moteur", CateCompetenceId = 9 },
                new SsCateCompetence { Intitule = "Remplacement des bougies", CateCompetenceId = 9 },
                new SsCateCompetence { Intitule = "Remplacement liquide de refroidissement", CateCompetenceId = 9 },

                new SsCateCompetence { Intitule = "Equilibrage", CateCompetenceId = 10 },
                new SsCateCompetence { Intitule = "Changer Roue", CateCompetenceId = 10 },
                new SsCateCompetence { Intitule = "Changer Pneu", CateCompetenceId = 10}

              
                );
            this.Competences.AddRange(
                new Competence { Id = 1, ProviderId = 1, SsCateCompetenceId = 6, NomSsCate = "Changer Roue", TarifHoraire = 40 },
                new Competence { Id = 2, ProviderId = 1, SsCateCompetenceId = 3, NomSsCate = "Changer la distribution", TarifHoraire = 50 },
                new Competence { Id = 3, ProviderId = 1, SsCateCompetenceId = 2, NomSsCate = "Changer la distribution", TarifHoraire = 17 },
                new Competence { Id = 4, ProviderId = 1, SsCateCompetenceId = 14, NomSsCate = "Changer la distribution", TarifHoraire = 44},

                new Competence { Id = 5, ProviderId = 2, SsCateCompetenceId = 1, NomSsCate = "Changer la distribution", TarifHoraire = 42 },
                new Competence { Id = 6, ProviderId = 2, SsCateCompetenceId = 3, NomSsCate = "Changer la distribution", TarifHoraire = 46 },
                new Competence { Id = 7, ProviderId = 2, SsCateCompetenceId = 9, NomSsCate = "Changer la distribution", TarifHoraire = 48 },
                new Competence { Id = 8, ProviderId = 2, SsCateCompetenceId = 6, NomSsCate = "Changer la distribution", TarifHoraire = 31 },
                new Competence { Id = 9, ProviderId = 2, SsCateCompetenceId = 15, NomSsCate = "Changer la distribution", TarifHoraire = 58 },
                new Competence { Id = 10, ProviderId = 2, SsCateCompetenceId = 20, NomSsCate = "Changer la distribution", TarifHoraire = 14 },

                new Competence { Id = 11, ProviderId = 3, SsCateCompetenceId = 2, NomSsCate = "Changer la distribution", TarifHoraire = 19 },
                new Competence { Id = 12, ProviderId = 3, SsCateCompetenceId = 6, NomSsCate = "Changer la distribution", TarifHoraire = 32 },
                new Competence { Id = 13, ProviderId = 3, SsCateCompetenceId = 1, NomSsCate = "Changer la distribution", TarifHoraire = 40 },
                new Competence { Id = 14, ProviderId = 3, SsCateCompetenceId = 14, NomSsCate = "Changer la distribution", TarifHoraire = 31 },
                new Competence { Id = 15, ProviderId = 3, SsCateCompetenceId = 16, NomSsCate = "Changer la distribution", TarifHoraire = 67 },
                new Competence { Id = 16, ProviderId = 3, SsCateCompetenceId = 22, NomSsCate = "Changer la distribution", TarifHoraire = 55 }

                );

            this.Ressources.AddRange(

                new Ressource
                {
                    Intitule = "Cle à molette",
                    Adresse = "124 Rue Hoche",
                    Categorie = CateRessource.OUTIL,
                    Disponible = true,
                    TarifJournalier = 15,
                    ProviderId = 1
                },

                  new Ressource
                  {
                      Intitule = "Vidangeuse",
                      Adresse = "1 Rue Boucher",
                      Categorie = CateRessource.OUTIL_SPECIALISE,
                      Disponible = true,
                      TarifJournalier = 40,
                      ProviderId = 2
                  },
                  
                  new Ressource

                  {
                      Intitule = "Garage TakoLef",
                      Adresse = "25 Rue de la Feuille, 85000, Konoha",
                      Categorie = CateRessource.LOCAL_GARAGE,
                      Disponible = true,
                      TarifJournalier = 100,
                      ProviderId = 2
                  },  
                  
                  new Ressource
                  {
                      Intitule = "Remorque 3m^3",
                      Adresse = "1 Rue Boucher",
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

            this.SaveChanges();
        }
    }
}