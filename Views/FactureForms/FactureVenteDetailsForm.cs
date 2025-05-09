using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FactureForms
{
    public partial class FactureVenteDetailsForm : Form
    {
        private readonly IClientRepository _clientRepository;
        private FactureVente _facture;
        private bool _isNewFacture;
        private List<Client> _clients;

        public FactureVenteDetailsForm(FactureVente facture = null)
        {
            InitializeComponent();
            
            // Obtenir le repository depuis le conteneur DI
            _clientRepository = Program.ServiceProvider.GetRequiredService<IClientRepository>();
            
            _facture = facture ?? new FactureVente { Id = Guid.NewGuid(), Date = DateTime.Now, LignesFacture = new List<LigneFacture>() };
            _isNewFacture = facture == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewFacture ? "Ajouter une facture de vente" : "Modifier une facture de vente";
            
            // Charger la liste des clients depuis la base de données
            LoadClientsAsync();
            
            // Charger les données de la facture si en mode édition
            if (!_isNewFacture)
            {
                this.dateTimePicker.Value = _facture.Date;
                
                // La sélection du client sera faite après le chargement des données
                
                // Afficher les lignes de facture s'il y en a
                if (_facture.LignesFacture != null && _facture.LignesFacture.Count > 0)
                {
                    RefreshLignesFactureGrid();
                }
            }
        }
        
        private async void LoadClientsAsync()
        {
            try
            {
                // Désactiver la ComboBox pendant le chargement
                clientComboBox.Enabled = false;
                
                // Obtenir les clients depuis la base de données
                var clients = await _clientRepository.GetAllAsync();
                _clients = new List<Client>(clients);
                
                // Mettre à jour la ComboBox
                clientComboBox.DataSource = _clients;
                clientComboBox.DisplayMember = "Nom";
                clientComboBox.ValueMember = "Id";
                
                // Réactiver la ComboBox
                clientComboBox.Enabled = true;
                
                // Sélectionner le client pour une facture existante
                if (!_isNewFacture)
                {
                    foreach (Client client in clientComboBox.Items)
                    {
                        if (client.Id == _facture.ClientId)
                        {
                            clientComboBox.SelectedItem = client;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des clients: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (clientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clientComboBox.Focus();
                return;
            }
            
            // Validation supplémentaire...
            
            // Mettre à jour l'objet facture avec les valeurs du formulaire
            _facture.Date = dateTimePicker.Value;
            
            // Récupérer l'ID du client sélectionné
            if (clientComboBox.SelectedItem is Client selectedClient)
            {
                _facture.ClientId = selectedClient.Id;
                _facture.Client = selectedClient;
            }
            
            // Définir le DialogResult pour indiquer le succès
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void AddLigneButton_Click(object sender, EventArgs e)
        {
            // Ouvrir le formulaire d'ajout de ligne
            var ligneForm = new LigneFactureForm();
            
            if (ligneForm.ShowDialog() == DialogResult.OK)
            {
                // Ajouter la ligne à la facture
                _facture.LignesFacture.Add(ligneForm.LigneFacture);
                
                // Lier la ligne à cette facture
                ligneForm.LigneFacture.FactureId = _facture.Id;
                ligneForm.LigneFacture.Facture = _facture;
                
                // Rafraîchir la grille des lignes
                RefreshLignesFactureGrid();
                
                // Créer un mouvement de stock (sortie) associé
                var mouvement = new MouvementStock
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Type = "SORTIE",
                    Quantite = ligneForm.LigneFacture.Quantite,
                    PieceId = ligneForm.LigneFacture.PieceId,
                    FactureId = _facture.Id
                };
                
                // Dans une vraie application, ce mouvement serait enregistré dans la base de données
                System.Diagnostics.Debug.WriteLine($"Mouvement de stock créé: {mouvement.Type} - {mouvement.Quantite} - Pièce {mouvement.PieceId}");
            }
        }
        
        private void RefreshLignesFactureGrid()
        {
            // Configurer les colonnes de la grille si nécessaire
            if (lignesDataGridView.Columns.Count == 0)
            {
                // Ajouter les colonnes uniquement si elles n'existent pas déjà
                lignesDataGridView.AutoGenerateColumns = false;
                
                var pieceColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Piece.Reference",
                    HeaderText = "Pièce",
                    Width = 150
                };
                
                var quantiteColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Quantite",
                    HeaderText = "Quantité",
                    Width = 70
                };
                
                var prixUnitaireColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PrixUnitaireHT",
                    HeaderText = "Prix unitaire HT",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                };
                
                var remiseColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "RemisePct",
                    HeaderText = "Remise %",
                    Width = 80
                };
                
                lignesDataGridView.Columns.AddRange(new DataGridViewColumn[] 
                {
                    pieceColumn, quantiteColumn, prixUnitaireColumn, remiseColumn
                });
            }
            
            // Mettre à jour la source de données de la grille des lignes
            lignesDataGridView.DataSource = null;
            lignesDataGridView.DataSource = _facture.LignesFacture;
        }
        
        // Propriété pour accéder aux données de la facture
        public FactureVente Facture => _facture;
    }
} 