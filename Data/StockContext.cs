using System;
using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;

namespace StockApp.Data
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
            // Enable detailed errors
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<FactureAchat> FacturesAchat { get; set; }
        public DbSet<FactureVente> FacturesVente { get; set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }
        public DbSet<MouvementStock> MouvementsStock { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<LigneFacture>()
                .HasOne(lf => lf.Facture)
                .WithMany(f => f.LignesFacture)
                .HasForeignKey(lf => lf.FactureId);

            modelBuilder.Entity<LigneFacture>()
                .HasOne(lf => lf.Piece)
                .WithMany(p => p.LignesFacture)
                .HasForeignKey(lf => lf.PieceId);

            modelBuilder.Entity<FactureAchat>()
                .HasOne(fa => fa.Fournisseur)
                .WithMany(f => f.FacturesAchat)
                .HasForeignKey(fa => fa.FournisseurId);

            modelBuilder.Entity<FactureVente>()
                .HasOne(fv => fv.Client)
                .WithMany(c => c.FacturesVente)
                .HasForeignKey(fv => fv.ClientId);

            modelBuilder.Entity<MouvementStock>()
                .HasOne(ms => ms.Piece)
                .WithMany(p => p.MouvementsStock)
                .HasForeignKey(ms => ms.PieceId);

            // Seed initial admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Fixed ID to prevent duplicate seeding
                    Username = "admin",
                    Password = "admin", // In a real app, use password hashing
                    PasswordHash = "admin", // In a real app, use password hashing
                    Prenom = "Admin",
                    Nom = "Admin",
                    Role = Role.ADMIN,
                    Actif = true
                }
            );
        }
    }
} 