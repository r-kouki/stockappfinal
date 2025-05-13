using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FournisseurForms
{
    public partial class FournisseurManagementForm : UserControl
    {
        private readonly IFournisseurRepository _fournisseurRepository;
        private readonly StockDataAdapter _dataAdapter;
        private BindingSource _fournisseursBindingSource;
        
        public FournisseurManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _fournisseurRepository = Program.ServiceProvider.GetRequiredService<IFournisseurRepository>();
            
            // Initialize the data adapter
            _dataAdapter = new StockDataAdapter();
            
            // Set up binding source
            _fournisseursBindingSource = new BindingSource();
            _fournisseursBindingSource.DataSource = _dataAdapter.GetDataSet();
            _fournisseursBindingSource.DataMember = "Fournisseurs";
            
            // Set up data binding
            fournisseursDataGridView.DataSource = _fournisseursBindingSource;
            
            // Configure columns to match the dataset fields
            ConfigureColumns();
            
            // Load data from database
            LoadDataAsync();
        }
        
        private void ConfigureColumns()
        {
            fournisseursDataGridView.AutoGenerateColumns = false;
            
            // Clear existing columns
            fournisseursDataGridView.Columns.Clear();
            
            // Add columns matching the dataset schema
            var idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            };
            
            var nomColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var adresseColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Adresse",
                HeaderText = "Adresse",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 200
            };
            
            var telephoneColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telephone",
                HeaderText = "Téléphone",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var emailColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            // Add columns to DataGridView
            fournisseursDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                idColumn,
                nomColumn,
                adresseColumn,
                telephoneColumn,
                emailColumn
            });
        }
        
        private async void LoadDataAsync()
        {
            try
            {
                // Fill the fournisseurs table in the dataset
                _dataAdapter.FillFournisseursTable();
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
                    
                    // Refresh the dataset
                    _dataAdapter.FillFournisseursTable();
                    
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
                // Get the selected fournisseur ID from the DataRowView
                var selectedRow = fournisseursDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var fournisseurId = selectedRow["Id"].ToString();
                    
                    // Fetch the actual fournisseur entity from the repository
                    var fournisseur = await _fournisseurRepository.GetByIdAsync(fournisseurId);
                    
                    if (fournisseur != null)
                    {
                        var detailsForm = new FournisseurDetailsForm(fournisseur);
                        
                        if (detailsForm.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                // Update the fournisseur in the database
                                await _fournisseurRepository.UpdateAsync(detailsForm.Fournisseur);
                                
                                // Refresh the dataset
                                _dataAdapter.FillFournisseursTable();
                                
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
                // Get the selected fournisseur ID from the DataRowView
                var selectedRow = fournisseursDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var fournisseurId = selectedRow["Id"].ToString();
                    var fournisseurName = selectedRow["Nom"].ToString();
                    
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le fournisseur {fournisseurName}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _fournisseurRepository.DeleteAsync(fournisseurId);
                            
                            // Refresh the dataset
                            _dataAdapter.FillFournisseursTable();
                            
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
    }
} 