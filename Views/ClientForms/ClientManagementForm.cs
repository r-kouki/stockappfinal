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

namespace StockApp.ClientForms
{
    public partial class ClientManagementForm : UserControl
    {
        private readonly IClientRepository _clientRepository;
        private List<Client> _clients = new List<Client>();
        
        public ClientManagementForm()
        {
            InitializeComponent();
            
            // Get the repository from DI
            _clientRepository = Program.ServiceProvider.GetRequiredService<IClientRepository>();
            
            // Load data from database asynchronously
            LoadDataAsync();
        }
        
        private async void LoadDataAsync()
        {
            // Load clients from database in a fire-and-forget manner
            await LoadClientsAsync();
        }
        
        private async Task LoadClientsAsync()
        {
            try
            {
                // Get clients from the database
                var clients = await _clientRepository.GetAllAsync();
                _clients = clients.ToList();
                
                // Refresh the DataGridView
                RefreshClientsGrid();
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
                    
                    // Reload clients from the database
                    await LoadClientsAsync();
                    
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
                // Get the selected client
                var selectedClient = this.clientsDataGridView.SelectedRows[0].DataBoundItem as Client;
                
                if (selectedClient != null)
                {
                    // Create a copy of the client for editing
                    var clientCopy = new Client
                    {
                        Id = selectedClient.Id,
                        Nom = selectedClient.Nom,
                        Prenom = selectedClient.Prenom,
                        MatFiscal = selectedClient.MatFiscal,
                        Adresse = selectedClient.Adresse,
                        Telephone = selectedClient.Telephone,
                        Credit = selectedClient.Credit
                    };
                    
                    var detailsForm = new ClientDetailsForm(clientCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Update the client in the database
                            await _clientRepository.UpdateAsync(detailsForm.Client);
                            
                            // Reload clients from the database
                            await LoadClientsAsync();
                            
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
                var selectedClient = this.clientsDataGridView.SelectedRows[0].DataBoundItem as Client;
                
                if (selectedClient != null)
                {
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le client {selectedClient.Nom} {selectedClient.Prenom}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Delete from the database
                            await _clientRepository.DeleteAsync(selectedClient.Id);
                            
                            // Reload clients from the database
                            await LoadClientsAsync();
                            
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
        
        private void RefreshClientsGrid()
        {
            // Update the DataSource to refresh the grid
            this.clientsDataGridView.DataSource = null;
            this.clientsDataGridView.DataSource = _clients;
        }
    }
} 