using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FournisseurForms
{
    public partial class FournisseurManagementForm : UserControl
    {
        private readonly IFournisseurRepository _fournisseurRepository;
        private List<Fournisseur> _fournisseurs = new List<Fournisseur>();
        
        public FournisseurManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _fournisseurRepository = Program.ServiceProvider.GetRequiredService<IFournisseurRepository>();
            
            // Load data from database asynchronously
            LoadDataAsync();
        }
        
        private async void LoadDataAsync()
        {
            // Load fournisseurs from database in a fire-and-forget manner
            await LoadFournisseursAsync();
        }
        
        private async Task LoadFournisseursAsync()
        {
            try
            {
                // Get fournisseurs from the database
                var fournisseurs = await _fournisseurRepository.GetAllAsync();
                _fournisseurs = fournisseurs.ToList();
                
                // Refresh the DataGridView
                RefreshFournisseursGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des fournisseurs: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new FournisseurDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Add the new fournisseur to the database
                    await _fournisseurRepository.AddAsync(detailsForm.Fournisseur);
                    
                    // Reload fournisseurs from the database
                    await LoadFournisseursAsync();
                    
                    MessageBox.Show("Fournisseur ajouté avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du fournisseur: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.fournisseursDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected fournisseur
                var selectedFournisseur = this.fournisseursDataGridView.SelectedRows[0].DataBoundItem as Fournisseur;
                
                if (selectedFournisseur != null)
                {
                    // Create a copy of the fournisseur for editing
                    var fournisseurCopy = new Fournisseur
                    {
                        Id = selectedFournisseur.Id,
                        Nom = selectedFournisseur.Nom,
                        Prenom = selectedFournisseur.Prenom,
                        MatFiscal = selectedFournisseur.MatFiscal,
                        Adresse = selectedFournisseur.Adresse,
                        Telephone = selectedFournisseur.Telephone,
                        Credit = selectedFournisseur.Credit
                    };
                    
                    var detailsForm = new FournisseurDetailsForm(fournisseurCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the fournisseur in the database
                            await _fournisseurRepository.UpdateAsync(detailsForm.Fournisseur);
                            
                            // Reload fournisseurs from the database
                            await LoadFournisseursAsync();
                            
                            MessageBox.Show("Fournisseur modifié avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la modification du fournisseur: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.fournisseursDataGridView.SelectedRows.Count > 0)
            {
                var selectedFournisseur = this.fournisseursDataGridView.SelectedRows[0].DataBoundItem as Fournisseur;
                
                if (selectedFournisseur != null)
                {
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le fournisseur {selectedFournisseur.Nom} {selectedFournisseur.Prenom}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _fournisseurRepository.DeleteAsync(selectedFournisseur.Id);
                            
                            // Reload fournisseurs from the database
                            await LoadFournisseursAsync();
                            
                            MessageBox.Show("Fournisseur supprimé avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression du fournisseur: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void RefreshFournisseursGrid()
        {
            // Update the DataSource to refresh the grid
            this.fournisseursDataGridView.DataSource = null;
            this.fournisseursDataGridView.DataSource = _fournisseurs;
        }
    }
} 