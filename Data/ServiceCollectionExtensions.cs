using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Data.Repositories;
using StockApp.Data.Services;
using Microsoft.Data.Sqlite;
using StockApp.Data.Entities;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

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
                bool success = false;
                
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<StockContext>();
                    
                    try
                    {
                        // Only create the database if it doesn't exist
                        bool created = context.Database.EnsureCreated();
                        
                        // Try adding CreatedAt column to the User table if it doesn't exist
                        try
                        {
                            var sql = "ALTER TABLE Users ADD COLUMN CreatedAt TEXT";
                            context.Database.ExecuteSqlRaw(sql);
                            System.Diagnostics.Debug.WriteLine("Added CreatedAt column to Users table");
                        }
                        catch (Exception ex)
                        {
                            // Column might already exist or other issue
                            System.Diagnostics.Debug.WriteLine($"Note: Could not add CreatedAt column: {ex.Message}");
                        }
                        
                        // Try adding Salt column to the User table if it doesn't exist
                        try
                        {
                            var sql = "ALTER TABLE Users ADD COLUMN Salt TEXT";
                            context.Database.ExecuteSqlRaw(sql);
                            System.Diagnostics.Debug.WriteLine("Added Salt column to Users table");
                        }
                        catch (Exception ex)
                        {
                            // Column might already exist or other issue
                            System.Diagnostics.Debug.WriteLine($"Note: Could not add Salt column: {ex.Message}");
                        }
                        
                        // Check if any users exist
                        bool anyUsers = false;
                        try 
                        {
                            anyUsers = context.Users.Any();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error checking for users: {ex.Message}");
                            // Try with raw SQL if EF Core approach fails
                            try
                            {
                                var result = context.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM Users");
                                anyUsers = result > 0;
                            }
                            catch
                            {
                                // Table may not exist or other issue
                                anyUsers = false;
                            }
                        }
                        
                        // If no users exist, create an admin user manually
                        if (!anyUsers)
                        {
                            try
                            {
                                // Generate a secure salt and hash for the admin password
                                string salt = GenerateSalt();
                                string passwordHash = HashPassword("admin", salt);
                                
                                // Get all User property names to check if CreatedAt exists
                                var userProperties = typeof(StockApp.Data.Entities.User).GetProperties().Select(p => p.Name).ToList();
                                var userToAdd = new Entities.User
                                {
                                    Id = "25UR000001", // Use the new ID format
                                    Username = "admin",
                                    Password = "admin", // Plain text for reference
                                    PasswordHash = passwordHash,
                                    Salt = salt,
                                    Nom = "Admin",
                                    Prenom = "System",
                                    Role = Entities.Role.ADMIN,
                                    Actif = true,
                                    CreatedAt = DateTime.Now // Set creation date explicitly
                                };
                                
                                // Only set CreatedAt if the property exists in the DB
                                //if (userProperties.Contains("CreatedAt"))
                                //{
                                //    userToAdd.CreatedAt = DateTime.Now;
                                //}
                                
                                context.Users.Add(userToAdd);
                                context.SaveChanges();
                                success = true;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error adding user with EF Core: {ex.Message}");
                                
                                // Fallback to raw SQL
                                try
                                {
                                    var sql = @"
                                    CREATE TABLE IF NOT EXISTS Users (
                                        Id TEXT PRIMARY KEY,
                                        Username TEXT NOT NULL,
                                        Password TEXT NOT NULL,
                                        PasswordHash TEXT,
                                        Salt TEXT,
                                        Nom TEXT,
                                        Prenom TEXT,
                                        Role INTEGER NOT NULL,
                                        Actif INTEGER NOT NULL,
                                        CreatedAt TEXT
                                    );
                                    
                                    INSERT OR IGNORE INTO Users (Id, Username, Password, PasswordHash, Salt, Nom, Prenom, Role, Actif, CreatedAt)
                                    VALUES ('25UR000001', 'admin', 'admin', '', '', 'Admin', 'System', 1, 1, datetime('now'));
                                    ";
                                    
                                    context.Database.ExecuteSqlRaw(sql);
                                    success = true;
                                }
                                catch (Exception sqlEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Error with SQL fallback: {sqlEx.Message}");
                                }
                            }
                        }
                        else
                        {
                            success = true;
                            // Update existing admin users to use the new security if they don't have a salt
                            // Using SQL to avoid issues with potential missing columns
                            try
                            {
                                var adminUsersToUpdate = context.Users
                                    .Where(u => u.Role == Entities.Role.ADMIN && string.IsNullOrEmpty(u.Salt))
                                    .ToList();
                                    
                                foreach (var admin in adminUsersToUpdate)
                                {
                                    admin.Salt = GenerateSalt();
                                    admin.PasswordHash = HashPassword(admin.Password, admin.Salt);
                                    
                                    // Only set CreatedAt if it exists
                                    var properties = typeof(StockApp.Data.Entities.User).GetProperties().Select(p => p.Name).ToList();
                                    if (properties.Contains("CreatedAt"))
                                    {
                                        admin.CreatedAt = DateTime.Now;
                                    }
                                }
                                
                                if (adminUsersToUpdate.Any())
                                {
                                    context.SaveChanges();
                                }
                                
                                // Add test users if they don't exist
                                // Check for poly user
                                var polyUserExists = context.Users.Any(u => u.Username == "poly");
                                if (!polyUserExists)
                                {
                                    string polySalt = GenerateSalt();
                                    string polyPasswordHash = HashPassword("poly", polySalt);
                                    
                                    var idGenerator = serviceProvider.GetRequiredService<IIdGeneratorService>();
                                    var polyUser = new Entities.User
                                    {
                                        Id = idGenerator.GenerateId("USER"),
                                        Username = "poly",
                                        Password = "poly",  // Plain text for reference
                                        PasswordHash = polyPasswordHash,
                                        Salt = polySalt,
                                        Nom = "Test",
                                        Prenom = "Admin",
                                        Role = Entities.Role.ADMIN,
                                        Actif = true,
                                        CreatedAt = DateTime.Now
                                    };
                                    
                                    context.Users.Add(polyUser);
                                    System.Diagnostics.Debug.WriteLine("Created poly/poly admin user");
                                }
                                
                                // Check for regular user
                                var regularUserExists = context.Users.Any(u => u.Username == "user");
                                if (!regularUserExists)
                                {
                                    string userSalt = GenerateSalt();
                                    string userPasswordHash = HashPassword("user", userSalt);
                                    
                                    var idGenerator = serviceProvider.GetRequiredService<IIdGeneratorService>();
                                    var regularUser = new Entities.User
                                    {
                                        Id = idGenerator.GenerateId("USER"),
                                        Username = "user",
                                        Password = "user",  // Plain text for reference
                                        PasswordHash = userPasswordHash,
                                        Salt = userSalt,
                                        Nom = "Test",
                                        Prenom = "User",
                                        Role = Entities.Role.OPERATEUR,
                                        Actif = true,
                                        CreatedAt = DateTime.Now
                                    };
                                    
                                    context.Users.Add(regularUser);
                                    System.Diagnostics.Debug.WriteLine("Created user/user operator user");
                                }
                                
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error updating admin users: {ex.Message}");
                                // Try with raw SQL if EF Core approach fails
                                try
                                {
                                    var sql = "UPDATE Users SET PasswordHash = @passwordHash, Salt = @salt WHERE Role = 1 AND (Salt IS NULL OR Salt = '')";
                                    string salt = GenerateSalt();
                                    string passwordHash = HashPassword("admin", salt);
                                    context.Database.ExecuteSqlRaw(sql, new Microsoft.Data.Sqlite.SqliteParameter("@passwordHash", passwordHash), 
                                                                      new Microsoft.Data.Sqlite.SqliteParameter("@salt", salt));
                                    
                                    // Try to add test users using raw SQL
                                    try
                                    {
                                        // Check if "poly" user exists
                                        var checkPolySql = "SELECT COUNT(*) FROM Users WHERE Username = 'poly'";
                                        var polyCount = context.Database.ExecuteSqlRaw(checkPolySql);
                                        
                                        if (polyCount == 0)
                                        {
                                            var polySql = @"
                                            INSERT OR IGNORE INTO Users (Id, Username, Password, PasswordHash, Salt, Nom, Prenom, Role, Actif, CreatedAt)
                                            VALUES ('25UR000002', 'poly', 'poly', '', '', 'Test', 'Admin', 1, 1, datetime('now'));
                                            ";
                                            context.Database.ExecuteSqlRaw(polySql);
                                            System.Diagnostics.Debug.WriteLine("Created poly user with direct SQL");
                                        }
                                        
                                        // Check if "user" user exists
                                        var checkUserSql = "SELECT COUNT(*) FROM Users WHERE Username = 'user'";
                                        var userCount = context.Database.ExecuteSqlRaw(checkUserSql);
                                        
                                        if (userCount == 0)
                                        {
                                            var userSql = @"
                                            INSERT OR IGNORE INTO Users (Id, Username, Password, PasswordHash, Salt, Nom, Prenom, Role, Actif, CreatedAt)
                                            VALUES ('25UR000003', 'user', 'user', '', '', 'Test', 'User', 1, 1, datetime('now'));
                                            ";
                                            context.Database.ExecuteSqlRaw(userSql);
                                            System.Diagnostics.Debug.WriteLine("Created user user with direct SQL");
                                        }
                                    }
                                    catch (Exception testUserEx)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Error adding test users with SQL: {testUserEx.Message}");
                                    }
                                }
                                catch (Exception sqlEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"SQL error updating admin users: {sqlEx.Message}");
                                }
                            }
                        }
                        
                        // Force save changes to disk
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error saving changes: {ex.Message}");
                        }
                        
                        // Update debug info
                        dbInfo += $"\nCreated: {created}\nExists after: {File.Exists(dbPath)}";
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                        // Last resort fallback - create a users table and add admin user directly
                        if (!success)
                        {
                            try
                            {
                                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                                {
                                    connection.Open();
                                    using (var cmd = connection.CreateCommand())
                                    {
                                        cmd.CommandText = @"
                                        CREATE TABLE IF NOT EXISTS Users (
                                            Id TEXT PRIMARY KEY,
                                            Username TEXT NOT NULL,
                                            Password TEXT NOT NULL,
                                            Role INTEGER NOT NULL,
                                            Actif INTEGER NOT NULL,
                                            CreatedAt TEXT
                                        );
                                        
                                        INSERT OR IGNORE INTO Users (Id, Username, Password, Role, Actif, CreatedAt)
                                        VALUES ('25UR000001', 'admin', 'admin', 1, 1, datetime('now'));
                                        ";
                                        cmd.ExecuteNonQuery();
                                    }
                                    connection.Close();
                                }
                                dbInfo += "\nCreated default admin user with direct SQL";
                                success = true;
                            }
                            catch (Exception sqlEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Last resort SQL error: {sqlEx.Message}");
                                dbInfo += $"\nError creating default user: {sqlEx.Message}";
                            }
                        }
                    }
                    
                    MessageBox.Show(dbInfo, "Database Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper methods for password security
        private static string GenerateSalt()
        {
            var saltBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        
        private static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
                
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            // Store the connection string for later use
            DbConnectionString = connectionString;
            
            // Register database context
            services.AddDbContext<StockContext>(options =>
                options.UseSqlite(connectionString)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            // Register repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFournisseurRepository, FournisseurRepository>();
            services.AddScoped<IPieceRepository, PieceRepository>();
            services.AddScoped<IFactureAchatRepository, FactureAchatRepository>();
            services.AddScoped<IFactureVenteRepository, FactureVenteRepository>();
            services.AddScoped<IMouvementStockRepository, MouvementStockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            // Register ID generation service
            services.AddSingleton<IIdGeneratorService, IdGeneratorService>();
            
            // Register auth service
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }

    // ID Generation service interfaces and implementation
    public interface IIdGeneratorService
    {
        string GenerateId(string entityType);
    }

    public class IdGeneratorService : IIdGeneratorService
    {
        private readonly object _lock = new();
        private readonly Dictionary<string, int> _counters = new();

        public string GenerateId(string entityType)
        {
            lock (_lock)
            {
                string prefix = GetYearPrefix();
                string typeCode = GetEntityTypeCode(entityType);
                
                if (!_counters.TryGetValue(entityType, out int counter))
                {
                    counter = 1;
                    _counters[entityType] = counter;
                }
                else
                {
                    counter++;
                    _counters[entityType] = counter;
                }
                
                return $"{prefix}{typeCode}{counter:D6}";
            }
        }

        private static string GetYearPrefix()
        {
            return DateTime.Now.ToString("yy"); // Last two digits of current year
        }

        private static string GetEntityTypeCode(string entityType)
        {
            return entityType.ToUpper() switch
            {
                "CLIENT" => "CL",
                "FOURNISSEUR" => "FR",
                "PIECE" => "PC",
                "FACTUREACHAT" => "FA",
                "FACTUREVENTE" => "FV",
                "USER" => "UR",
                _ => "XX" // Default/unknown type
            };
        }
    }
} 