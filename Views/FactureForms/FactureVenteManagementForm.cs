using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FactureForms
{
    public partial class FactureVenteManagementForm : UserControl
    {
        private readonly IFactureVenteRepository _factureRepository;
        private List<FactureVente> _factures = new List<FactureVente>();
        
        public FactureVenteManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _factureRepository = Program.ServiceProvider.GetRequiredService<IFactureVenteRepository>();
            
            // Load data from database asynchronously
            LoadDataAsync();
            
            // Vérifier les permissions de suppression selon le rôle
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                deleteButton.Enabled = false;
                deleteButton.Text = "Supprimer (Admin uniquement)";
            }
        }
        
        private async void LoadDataAsync()
        {
            // Load factures from database in a fire-and-forget manner
            await LoadFacturesAsync();
        }
        
        private async Task LoadFacturesAsync()
        {
            try
            {
                // Get factures from the database
                var factures = await _factureRepository.GetAllAsync();
                _factures = new List<FactureVente>(factures);
                
                // Refresh the DataGridView
                RefreshFacturesGrid();
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
                    // Add the new facture to the database
                    await _factureRepository.AddAsync(detailsForm.Facture);
                    
                    // Reload factures from the database
                    await LoadFacturesAsync();
                    
                    // Enregistrer l'action dans le log
                    LogActivity($"Ajout de la facture de vente {detailsForm.Facture.Id}");
                    
                    MessageBox.Show("Facture de vente ajoutée avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout de la facture: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.facturesDataGridView.SelectedRows.Count > 0)
            {
                // Récupérer la facture sélectionnée
                var selectedFacture = this.facturesDataGridView.SelectedRows[0].DataBoundItem as FactureVente;
                
                if (selectedFacture != null)
                {
                    // Créer une copie de la facture pour l'édition
                    var factureCopy = new FactureVente
                    {
                        Id = selectedFacture.Id,
                        Date = selectedFacture.Date,
                        ClientId = selectedFacture.ClientId
                    };
                    
                    var detailsForm = new FactureVenteDetailsForm(factureCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the facture in the database
                            await _factureRepository.UpdateAsync(detailsForm.Facture);
                            
                            // Reload factures from the database
                            await LoadFacturesAsync();
                            
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
            else
            {
                MessageBox.Show("Veuillez sélectionner une facture à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
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
                var selectedFacture = this.facturesDataGridView.SelectedRows[0].DataBoundItem as FactureVente;
                
                if (selectedFacture != null)
                {
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la facture de vente du {selectedFacture.Date.ToShortDateString()}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _factureRepository.DeleteAsync(selectedFacture.Id);
                            
                            // Reload factures from the database
                            await LoadFacturesAsync();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Suppression de la facture de vente {selectedFacture.Id}");
                            
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
        
        private void RefreshFacturesGrid()
        {
            // Mettre à jour la source de données pour rafraîchir la grille
            this.facturesDataGridView.DataSource = null;
            this.facturesDataGridView.DataSource = _factures;
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
    }
} 