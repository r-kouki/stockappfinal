using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Data.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StockApp.Data
{
    public static class ServiceCollectionExtensions
    {
        // The connection string is stored as a static field to ensure consistency
        public static string DbConnectionString { get; private set; }
        
        public static IServiceCollection AddStockData(this IServiceCollection services, string connectionString)
        {
            // Store the connection string for later use
            DbConnectionString = connectionString;
            
            // Add DbContext
            services.AddDbContext<StockContext>(options =>
                options.UseSqlite(connectionString));

            // Add repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFournisseurRepository, FournisseurRepository>();
            services.AddScoped<IPieceRepository, PieceRepository>();
            services.AddScoped<IFactureAchatRepository, FactureAchatRepository>();
            services.AddScoped<IFactureVenteRepository, FactureVenteRepository>();
            services.AddScoped<IMouvementStockRepository, MouvementStockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
        
        public static void EnsureDatabaseCreated(this IServiceProvider serviceProvider)
        {
            try
            {
                // Extract database path from connection string
                string dbPath = DbConnectionString.Replace("Data Source=", "");
                
                // Log database information for debugging
                string dbInfo = $"Database path: {dbPath}\nExists before: {File.Exists(dbPath)}";
                
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<StockContext>();
                    
                    // Only create the database if it doesn't exist
                    bool created = context.Database.EnsureCreated();
                    
                    // If no users exist, create an admin user manually
                    if (!context.Users.Any())
                    {
                        context.Users.Add(new Entities.User
                        {
                            Id = Guid.NewGuid(),
                            Username = "admin",
                            Password = "admin",
                            PasswordHash = "admin",
                            Nom = "Admin",
                            Prenom = "System",
                            Role = Entities.Role.ADMIN,
                            Actif = true
                        });
                        
                        context.SaveChanges();
                    }
                    
                    // Force save changes to disk
                    context.SaveChanges();
                    
                    // Update debug info
                    dbInfo += $"\nCreated: {created}\nExists after: {File.Exists(dbPath)}";
                    MessageBox.Show(dbInfo, "Database Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 