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
                });

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
                      AdherentId = 2

                  },
                  new CompteUser
                  {
                      Mail = "Zawartoski.Valentin@gmail.com",
                      MotDePasse = dal.EncodeMD5("ZeFrontiste"),
                      Description = "Ma RAM rame...",
                      EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                      AdherentId = 3
                  },
                   new CompteUser
                   {
                       Mail = "Anthony.Dauphin@gmail.com",
                       MotDePasse = dal.EncodeMD5("ElBrutos"),
                       Description = "I HAVE NO IDEA WHAT I'M DOING",
                       EtatProfil = EtatProfil.ATTENTE_VALIDATION,
                       AdherentId = 4

                   });

            this.SaveChanges();

        }
    }
}
