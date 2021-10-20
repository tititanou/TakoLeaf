using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakoLeaf.Models
{
    public class BddContext : DbContext
    {

        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Provider> Providers {get;set;}
        public DbSet<Devis> Devis {get;set;}
        public DbSet<CompteUser> CompteUsers {get;set;}
        public DbSet<Message> Messages {get;set;}
        public DbSet<Admin> Admins {get;set;}
        public DbSet<PieceJustificative> PieceJustificatives {get;set;}
        public DbSet<Consumer> Consumers {get;set;}
        public DbSet<Sujet> Sujets {get;set;}
        public DbSet<Ressource> Ressources {get;set;}
        public DbSet<Competence> Competences {get;set;}
        public DbSet<Voiture> Voitures {get;set;}
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Carte> Cartes {get;set;}
        public DbSet<Historique> Historiques {get;set;}
        public DbSet<Prestation> Prestations {get;set;}
        public DbSet<Avis> Avis { get; set; }
        public DbSet<DemandeDevis> DemandeDevis { get; set; }
        public DbSet<CateCompetence> CateCompetences { get; set; }
        public DbSet<SsCateCompetence> SsCateCompetences { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Rib> Ribs {get;set;}
        public DbSet<Post> Posts{get;set;}
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
            
        }
    }
}
