using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.MouvementStockForms
{
    public partial class MouvementStockManagementForm : UserControl
    {
        private readonly IMouvementStockRepository _mouvementRepository;
        private List<MouvementStock> _mouvements = new List<MouvementStock>();
        
        public MouvementStockManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _mouvementRepository = Program.ServiceProvider.GetRequiredService<IMouvementStockRepository>();
            
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
            // Load mouvements from database in a fire-and-forget manner
            await LoadMouvementsAsync();
        }
        
        private async Task LoadMouvementsAsync()
        {
            try
            {
                // Get mouvements from the database
                var mouvements = await _mouvementRepository.GetAllAsync();
                _mouvements = new List<MouvementStock>(mouvements);
                
                // Refresh the DataGridView
                RefreshMouvementsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des mouvements: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new MouvementStockDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Add the new mouvement to the database
                    await _mouvementRepository.AddAsync(detailsForm.Mouvement);
                    
                    // Reload mouvements from the database
                    await LoadMouvementsAsync();
                    
                    // Enregistrer l'action dans le log
                    LogActivity($"Ajout du mouvement de stock {detailsForm.Mouvement.Id}");
                    
                    MessageBox.Show("Mouvement de stock ajouté avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du mouvement: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.mouvementsDataGridView.SelectedRows.Count > 0)
            {
                // Récupérer le mouvement sélectionné
                var selectedMouvement = this.mouvementsDataGridView.SelectedRows[0].DataBoundItem as MouvementStock;
                
                if (selectedMouvement != null)
                {
                    // Créer une copie du mouvement pour l'édition
                    var mouvementCopy = new MouvementStock
                    {
                        Id = selectedMouvement.Id,
                        Date = selectedMouvement.Date,
                        Type = selectedMouvement.Type,
                        Quantite = selectedMouvement.Quantite,
                        PieceId = selectedMouvement.PieceId,
                        FactureId = selectedMouvement.FactureId
                    };
                    
                    var detailsForm = new MouvementStockDetailsForm(mouvementCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the mouvement in the database
                            await _mouvementRepository.UpdateAsync(detailsForm.Mouvement);
                            
                            // Reload mouvements from the database
                            await LoadMouvementsAsync();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Modification du mouvement de stock {detailsForm.Mouvement.Id}");
                            
                            MessageBox.Show("Mouvement de stock modifié avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la modification du mouvement: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un mouvement à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                MessageBox.Show("Seuls les administrateurs peuvent supprimer des mouvements de stock.", 
                    "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (this.mouvementsDataGridView.SelectedRows.Count > 0)
            {
                var selectedMouvement = this.mouvementsDataGridView.SelectedRows[0].DataBoundItem as MouvementStock;
                
                if (selectedMouvement != null)
                {
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le mouvement de stock du {selectedMouvement.Date.ToShortDateString()}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _mouvementRepository.DeleteAsync(selectedMouvement.Id);
                            
                            // Reload mouvements from the database
                            await LoadMouvementsAsync();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Suppression du mouvement de stock {selectedMouvement.Id}");
                            
                            MessageBox.Show("Mouvement de stock supprimé avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression du mouvement: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un mouvement à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void RefreshMouvementsGrid()
        {
            // Mettre à jour la source de données pour rafraîchir la grille
            this.mouvementsDataGridView.DataSource = null;
            this.mouvementsDataGridView.DataSource = _mouvements;
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