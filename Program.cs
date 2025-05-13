using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows.Forms;
using StockApp.Data;
using StockApp.FactureForms;
using StockApp.FournisseurForms;
using StockApp.ClientForms;
using StockApp.MouvementStockForms;
using StockApp.PieceForms;
using StockApp.UtilisateurForms;
using Microsoft.EntityFrameworkCore;
using StockApp.Data.Repositories;
using StockApp.Data.Entities;
using System.Threading.Tasks;

namespace StockApp
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        
        // Fixed path for database file to ensure consistency
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "StockApp",
            "stockapp.db");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath));
            
            ConfigureServices();
            
            // Show login form first
            var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // If login is successful, show the main form
                Application.Run(ServiceProvider.GetRequiredService<MainForm>());
            }
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Add data services using the fixed DB path
            services.RegisterDataServices($"Data Source={DbPath}");

            // Register Forms
            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();

            // Client forms
            services.AddTransient<ClientManagementForm>();
            services.AddTransient<ClientDetailsForm>();

            // Fournisseur forms
            services.AddTransient<FournisseurManagementForm>();
            services.AddTransient<FournisseurDetailsForm>();

            // Piece forms
            services.AddTransient<PieceManagementForm>();
            services.AddTransient<PieceDetailsForm>();

            // Facture forms
            services.AddTransient<FactureAchatManagementForm>();
            services.AddTransient<FactureAchatDetailsForm>();
            services.AddTransient<FactureVenteManagementForm>();
            services.AddTransient<FactureVenteDetailsForm>();
            services.AddTransient<LigneFactureForm>();

            // MouvementStock forms
            services.AddTransient<MouvementStockManagementForm>();
            services.AddTransient<MouvementStockDetailsForm>();

            // Utilisateur forms
            services.AddTransient<UtilisateurManagementForm>();
            services.AddTransient<UtilisateurDetailsForm>();
            
            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();
            
            // Ensure the database is created and seeded
            ServiceProvider.EnsureDatabaseCreated();
        }
        
        private static async Task CreateTestUsersAsync()
        {
            try
            {
                using var scope = ServiceProvider.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                
                // Check if poly user exists
                var polyUser = await userRepository.FindByUsernameAsync("poly");
                if (polyUser == null)
                {
                    // Create poly/poly admin user
                    var newPolyUser = new User
                    {
                        Username = "poly",
                        Password = "poly",
                        Nom = "Test",
                        Prenom = "Admin",
                        Role = Role.ADMIN,
                        Actif = true
                    };
                    await userRepository.AddAsync(newPolyUser);
                    System.Diagnostics.Debug.WriteLine("Created poly/poly admin user");
                }
                
                // Check if regular user exists
                var regularUser = await userRepository.FindByUsernameAsync("user");
                if (regularUser == null)
                {
                    // Create user/user operator user
                    var newRegularUser = new User
                    {
                        Username = "user",
                        Password = "user",
                        Nom = "Test",
                        Prenom = "User",
                        Role = Role.OPERATEUR,
                        Actif = true
                    };
                    await userRepository.AddAsync(newRegularUser);
                    System.Diagnostics.Debug.WriteLine("Created user/user operator user");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating test users: {ex.Message}");
                MessageBox.Show($"Error creating test users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Continue with application startup even if user creation fails
            }
        }
    }
}