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

namespace StockApp.ClientForms
{
    public partial class ClientManagementForm : UserControl
    {
        private readonly IClientRepository _clientRepository;
        private readonly StockDataAdapter _dataAdapter;
        private BindingSource _clientsBindingSource;
        
        public ClientManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _clientRepository = Program.ServiceProvider.GetRequiredService<IClientRepository>();
            
            // Initialize the data adapter
            _dataAdapter = new StockDataAdapter();
            
            // Set up binding source
            _clientsBindingSource = new BindingSource();
            _clientsBindingSource.DataSource = _dataAdapter.GetDataSet();
            _clientsBindingSource.DataMember = "Clients";
            
            // Set up data binding
            clientsDataGridView.DataSource = _clientsBindingSource;
            
            // Configure columns to match the dataset fields
            ConfigureColumns();
            
            // Load data from database
            LoadDataAsync();
        }
        
        private void ConfigureColumns()
        {
            clientsDataGridView.AutoGenerateColumns = false;
            
            // Clear existing columns
            clientsDataGridView.Columns.Clear();
            
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
            clientsDataGridView.Columns.AddRange(new DataGridViewColumn[] {
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
                // Fill the clients table in the dataset
                _dataAdapter.FillClientsTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des clients: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new ClientDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Add the new client to the database
                    await _clientRepository.AddAsync(detailsForm.Client);
                    
                    // Refresh the dataset
                    _dataAdapter.FillClientsTable();
                    
                    MessageBox.Show("Client ajouté avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du client: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.clientsDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected client ID from the DataRowView
                var selectedRow = clientsDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var clientId = selectedRow["Id"].ToString();
                    
                    // Fetch the actual client entity from the repository
                    var client = await _clientRepository.GetByIdAsync(clientId);
                    
                    if (client != null)
                    {
                        var detailsForm = new ClientDetailsForm(client);
                        
                        if (detailsForm.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                // Update the client in the database
                                await _clientRepository.UpdateAsync(detailsForm.Client);
                                
                                // Refresh the dataset
                                _dataAdapter.FillClientsTable();
                                
                                MessageBox.Show("Client modifié avec succès!", "Succès", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erreur lors de la modification du client: {ex.Message}", 
                                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.clientsDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected client ID from the DataRowView
                var selectedRow = clientsDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                if (selectedRow != null)
                {
                    var clientId = selectedRow["Id"].ToString();
                    var clientName = selectedRow["Nom"].ToString();
                    
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le client {clientName}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _clientRepository.DeleteAsync(clientId);
                            
                            // Refresh the dataset
                            _dataAdapter.FillClientsTable();
                            
                            MessageBox.Show("Client supprimé avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur lors de la suppression du client: {ex.Message}", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
} 