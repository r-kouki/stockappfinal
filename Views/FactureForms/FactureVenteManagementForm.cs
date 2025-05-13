using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using StockApp.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StockApp.FactureForms
{
    public partial class FactureVenteManagementForm : UserControl
    {
        private readonly IFactureVenteRepository _factureRepository;
        private readonly StockDataAdapter _dataAdapter;
        
        // Fix potential missing field declarations
        private StockApp.Data.StockDataSet stockDataSet;
        private System.Windows.Forms.BindingSource facturesVenteBindingSource;
        
        public FactureVenteManagementForm()
        {
            // Initialize components first
            InitializeComponent();
            
            // Get the repository from DI
            _factureRepository = Program.ServiceProvider.GetRequiredService<IFactureVenteRepository>();
            
            // Initialize the data adapter
            _dataAdapter = new StockDataAdapter();
            
            // Vérifier que facturesVenteBindingSource n'est pas null
            if (facturesVenteBindingSource != null)
            {
                // Set the data source
                facturesVenteBindingSource.DataMember = "FacturesVente";
                facturesVenteBindingSource.DataSource = _dataAdapter.GetDataSet();
            }
            else
            {
                // Si le binding source est null, créer une nouvelle instance
                facturesVenteBindingSource = new System.Windows.Forms.BindingSource();
                facturesVenteBindingSource.DataMember = "FacturesVente";
                facturesVenteBindingSource.DataSource = _dataAdapter.GetDataSet();
                
                // Connecter le binding source au DataGridView
                if (facturesDataGridView != null)
                {
                    facturesDataGridView.DataSource = facturesVenteBindingSource;
                }
            }
            
            // Load data from database
            LoadDataAsync();
            
            // Vérifier les permissions de suppression selon le rôle
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                deleteButton.Enabled = false;
                deleteButton.Text = "Supprimer (Admin uniquement)";
            }
        }
        
        // Fix LoadDataAsync to not be async since it doesn't await anything
        private void LoadDataAsync()
        {
            try
            {
                // Avant de charger les factures, assurons-nous que les clients sont chargés
                _dataAdapter.FillClientsTable();
                
                // Désactiver temporairement les contraintes au niveau du DataSet
                var dataSet = _dataAdapter.GetDataSet();
                bool previousEnforceConstraints = dataSet.EnforceConstraints;
                dataSet.EnforceConstraints = false;
                
                // Maintenant charger les factures
                _dataAdapter.FillFacturesVenteTable();
                
                // Réactiver les contraintes
                dataSet.EnforceConstraints = previousEnforceConstraints;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des factures: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new FactureVenteDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Afficher les détails de la facture pour le débogage
                    System.Diagnostics.Debug.WriteLine($"Début de l'ajout de facture - ClientId: '{detailsForm.Facture.ClientId}', " +
                        $"Lignes: {detailsForm.Facture.LignesFacture?.Count ?? 0}");
                    
                    // Vérifier que les données sont valides
                    if (string.IsNullOrEmpty(detailsForm.Facture.ClientId))
                    {
                        MessageBox.Show("L'ID du client est vide ou invalide.", "Erreur de validation", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if (detailsForm.Facture.LignesFacture == null || detailsForm.Facture.LignesFacture.Count == 0)
                    {
                        MessageBox.Show("La facture ne contient aucune ligne.", "Erreur de validation", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Vérifier que toutes les lignes ont un PieceId valide
                    foreach (var ligne in detailsForm.Facture.LignesFacture)
                    {
                        if (string.IsNullOrEmpty(ligne.PieceId))
                        {
                            MessageBox.Show("Une ligne de facture a un ID de pièce non valide.", 
                                "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        if (ligne.Quantite <= 0)
                        {
                            MessageBox.Show($"La quantité pour la pièce {ligne.PieceId} doit être supérieure à zéro.",
                                "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    
                    // Add the new facture to the database
                    await Task.Run(() => _factureRepository.AddAsync(detailsForm.Facture)).ConfigureAwait(true);
                    
                    // Refresh the dataset more safely
                    try {
                        // Instead of calling individual methods, use FillAllTables to properly handle constraints
                        _dataAdapter.FillAllTables();
                    }
                    catch (Exception refreshEx) {
                        System.Diagnostics.Debug.WriteLine($"Erreur lors du rafraîchissement des données: {refreshEx.Message}");
                        // Continuez même s'il y a une erreur de rafraîchissement pour ne pas perdre la facture
                    }
                    
                    // Enregistrer l'action dans le log
                    LogActivity($"Ajout de la facture de vente {detailsForm.Facture.Id}");
                    
                    MessageBox.Show("Facture de vente ajoutée avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Capturer et afficher tous les détails de l'erreur
                    var errorDetails = new StringBuilder();
                    errorDetails.AppendLine("Erreur lors de l'ajout de la facture:");
                    errorDetails.AppendLine(ex.Message);
                    
                    if (ex.InnerException != null)
                    {
                        errorDetails.AppendLine("\nDétails supplémentaires:");
                        errorDetails.AppendLine(ex.InnerException.Message);
                        
                        // Si l'exception interne est une DbUpdateException, essayer d'extraire plus de détails
                        if (ex.InnerException is DbUpdateException dbEx)
                        {
                            if (dbEx.InnerException != null)
                            {
                                errorDetails.AppendLine("\nErreur de base de données:");
                                errorDetails.AppendLine(dbEx.InnerException.Message);
                            }
                            
                            // Analyser les entrées affectées
                            errorDetails.AppendLine("\nEntités affectées:");
                            foreach (var entry in dbEx.Entries)
                            {
                                errorDetails.AppendLine($"- Type: {entry.Entity.GetType().Name}, État: {entry.State}");
                                
                                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                                {
                                    foreach (var prop in entry.Properties)
                                    {
                                        var value = prop.CurrentValue ?? "null";
                                        errorDetails.AppendLine($"  {prop.Metadata.Name}: {value}");
                                    }
                                }
                            }
                        }
                    }
                    
                    // Log the detailed error
                    System.Diagnostics.Debug.WriteLine("ERREUR DÉTAILLÉE:\n" + errorDetails.ToString());
                    
                    // Show the error dialog
                    ShowErrorDialog("Erreur d'ajout de facture", errorDetails.ToString(), ex);
                }
            }
        }
        
        // Fix EditButton_Click method to properly use async/await
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.facturesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected facture ID from the DataRowView
                var selectedRow = facturesDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var factureId = selectedRow["Id"].ToString();
                    
                    // Fetch the actual facture entity from the repository using await
                    try
                    {
                        var facture = await Task.Run(() => _factureRepository.GetByIdAsync(factureId)).ConfigureAwait(true);
                        
                        if (facture != null)
                        {
                            var detailsForm = new FactureVenteDetailsForm(facture);
                            
                            if (detailsForm.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    // Update the facture in the database
                                    await Task.Run(() => _factureRepository.UpdateAsync(detailsForm.Facture)).ConfigureAwait(true);
                                    
                                    // Refresh the dataset
                                    _dataAdapter.FillFacturesVenteTable();
                                    
                                    // Enregistrer l'action dans le log
                                    LogActivity($"Modification de la facture de vente {detailsForm.Facture.Id}");
                                    
                                    MessageBox.Show("Facture de vente modifiée avec succès!", "Succès", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Erreur lors de la modification de la facture: {ex.Message}", 
                                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la récupération de la facture: {ex.Message}", 
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une facture à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        // Fix DeleteButton_Click method to properly use async/await
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                MessageBox.Show("Seuls les administrateurs peuvent supprimer des factures.", 
                    "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (this.facturesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected facture ID from the DataRowView
                var selectedRow = facturesDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var factureId = selectedRow["Id"].ToString();
                    var factureDate = Convert.ToDateTime(selectedRow["Date"]).ToShortDateString();
                    
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la facture de vente du {factureDate}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database using Task.Run to make it truly async
                            await Task.Run(() => _factureRepository.DeleteAsync(factureId)).ConfigureAwait(true);
                            
                            // Refresh the dataset
                            _dataAdapter.FillFacturesVenteTable();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Suppression de la facture de vente {factureId}");
                            
                            MessageBox.Show("Facture de vente supprimée avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression de la facture: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une facture à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void LogActivity(string activity)
        {
            try
            {
                // Dans une application réelle, vous enregistreriez dans une base de données ou un fichier
                string logEntry = $"{DateTime.Now} - {LoginForm.CurrentUser.Username} - {activity}";
                System.Diagnostics.Debug.WriteLine(logEntry);
                
                // Ajouter au fichier de log
                string logPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "activity_log.txt");
                
                System.IO.File.AppendAllText(logPath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur d'enregistrement dans le log: {ex.Message}");
            }
        }

        private async Task LoadFacturesAsync()
        {
            try
            {
                // Use Task.Run to make this an actual async operation 
                await Task.Run(() => _dataAdapter.FillFacturesVenteTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des factures: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to show detailed error information
        private void ShowErrorDialog(string title, string message, Exception ex)
        {
            // Create a form to display detailed error information
            using (Form errorForm = new Form())
            {
                errorForm.Text = title;
                errorForm.Size = new System.Drawing.Size(700, 500);
                errorForm.StartPosition = FormStartPosition.CenterParent;
                errorForm.MinimizeBox = false;
                errorForm.MaximizeBox = false;
                errorForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                
                // Create text box for error details
                TextBox errorTextBox = new TextBox();
                errorTextBox.Multiline = true;
                errorTextBox.ReadOnly = true;
                errorTextBox.ScrollBars = ScrollBars.Vertical;
                errorTextBox.Dock = DockStyle.Fill;
                errorTextBox.Font = new System.Drawing.Font("Consolas", 9);
                
                // Build detailed error message including stack trace
                string fullErrorDetails = message + "\n\n";
                fullErrorDetails += "===== DÉTAILS TECHNIQUES =====\n";
                fullErrorDetails += $"Exception Type: {ex.GetType().FullName}\n";
                fullErrorDetails += $"Stack Trace:\n{ex.StackTrace}\n\n";
                
                // Add inner exception details if available
                if (ex.InnerException != null)
                {
                    fullErrorDetails += $"Inner Exception: {ex.InnerException.GetType().FullName}\n";
                    fullErrorDetails += $"Message: {ex.InnerException.Message}\n";
                    fullErrorDetails += $"Stack Trace:\n{ex.InnerException.StackTrace}\n";
                }
                
                errorTextBox.Text = fullErrorDetails;
                
                // Add controls to form
                errorForm.Controls.Add(errorTextBox);
                
                // Show the error dialog
                errorForm.ShowDialog();
            }
        }
    }
} 