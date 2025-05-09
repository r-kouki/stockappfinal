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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath));
            
            ConfigureServices();
            
            // Ensure the database is created and seeded
            ServiceProvider.EnsureDatabaseCreated();
            
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
            services.AddStockData($"Data Source={DbPath}");

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
        }
    }
}