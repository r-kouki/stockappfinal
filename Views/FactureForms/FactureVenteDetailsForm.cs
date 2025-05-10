using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StockApp.Data;

namespace StockApp.FactureForms
{
    public partial class FactureVenteDetailsForm : Form
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMouvementStockRepository _mouvementStockRepository;
        private readonly IPieceRepository _pieceRepository;
        private FactureVente _facture;
        private bool _isNewFacture;
        private List<Client> _clients;

        public FactureVenteDetailsForm(FactureVente facture = null)
        {
            InitializeComponent();
            
            // Obtenir les repositories depuis le conteneur DI
            _clientRepository = Program.ServiceProvider.GetRequiredService<IClientRepository>();
            _mouvementStockRepository = Program.ServiceProvider.GetRequiredService<IMouvementStockRepository>();
            _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
            
            _facture = facture ?? new FactureVente { 
                Id = string.Empty, 
                Date = DateTime.Now, 
                DateEcheance = DateTime.Now.AddDays(30), 
                LignesFacture = new List<LigneFacture>() 
            };
            _isNewFacture = facture == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewFacture ? "Ajouter une facture de vente" : "Modifier une facture de vente";
            
            // Charger la liste des clients depuis la base de données
            LoadClientsAsync();
            
            // Charger les données de la facture si en mode édition
            if (!_isNewFacture)
            {
                this.dateTimePicker.Value = _facture.Date;
                
                // Set DateEcheance if it has a value
                if (_facture.DateEcheance.HasValue)
                {
                    this.dateEcheanceTimePicker.Value = _facture.DateEcheance.Value;
                }
                else
                {
                    // Default to 30 days from invoice date
                    this.dateEcheanceTimePicker.Value = _facture.Date.AddDays(30);
                }
                
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
        
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (clientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clientComboBox.Focus();
                return;
            }
            
            // Vérifier qu'il y a au moins une ligne de facture
            if (_facture.LignesFacture == null || _facture.LignesFacture.Count == 0)
            {
                MessageBox.Show("Vous devez ajouter au moins une ligne à la facture.", 
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Créer une nouvelle instance de facture plutôt que de modifier l'existante
                // Cette approche évite les problèmes de suivi d'entité
                var newFacture = new FactureVente
                {
                    Id = _facture.Id,
                    Date = dateTimePicker.Value,
                    DateEcheance = dateEcheanceTimePicker.Value,
                    MontantPaye = _facture.MontantPaye,
                    Note = _facture.Note,
                    NumeroFactureClient = _facture.NumeroFactureClient
                };
                
                // Récupérer l'ID du client sélectionné, mais ne pas stocker la référence à l'objet Client
                if (clientComboBox.SelectedItem is Client selectedClient)
                {
                    newFacture.ClientId = selectedClient.Id;
                }
                
                // Créer des copies des lignes de facture pour éviter les problèmes de suivi d'entité
                newFacture.LignesFacture = new List<LigneFacture>();
                foreach (var ligne in _facture.LignesFacture)
                {
                    var newLigne = new LigneFacture
                    {
                        Id = ligne.Id,
                        Quantite = ligne.Quantite,
                        PrixUnitaireHT = ligne.PrixUnitaireHT,
                        RemisePct = ligne.RemisePct,
                        PieceId = ligne.PieceId,
                        FactureId = newFacture.Id
                    };
                    
                    newFacture.LignesFacture.Add(newLigne);
                }
                
                // Remplacer la facture actuelle par la nouvelle
                _facture = newFacture;
                
                // Définir le DialogResult pour indiquer le succès
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement de la facture: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private async void AddLigneButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ouvrir le formulaire d'ajout de ligne
                var ligneForm = new LigneFactureForm();
                
                if (ligneForm.ShowDialog() == DialogResult.OK)
                {
                    // Vérifier que la pièce existe
                    if (ligneForm.LigneFacture == null || string.IsNullOrEmpty(ligneForm.LigneFacture.PieceId))
                    {
                        MessageBox.Show("Données de ligne invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    try
                    {
                        // Vérifier le stock disponible
                        var piece = await _pieceRepository.GetByIdAsync(ligneForm.LigneFacture.PieceId);
                        if (piece == null)
                        {
                            MessageBox.Show("Pièce non trouvée dans la base de données.", 
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        if (piece.Stock < ligneForm.LigneFacture.Quantite)
                        {
                            MessageBox.Show($"Stock insuffisant. Stock disponible: {piece.Stock}, Quantité demandée: {ligneForm.LigneFacture.Quantite}", 
                                "Stock insuffisant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        // Créer une nouvelle ligne plutôt que d'utiliser directement celle du formulaire
                        // Cette approche évite les problèmes de suivi d'entité
                        var nouvelleLigne = new LigneFacture
                        {
                            Id = string.Empty, // L'ID sera généré par le repository
                            PieceId = ligneForm.LigneFacture.PieceId,
                            Quantite = ligneForm.LigneFacture.Quantite,
                            PrixUnitaireHT = ligneForm.LigneFacture.PrixUnitaireHT,
                            RemisePct = ligneForm.LigneFacture.RemisePct,
                            FactureId = _facture.Id
                        };
                        
                        // Ajouter la ligne à la facture
                        if (_facture.LignesFacture == null)
                        {
                            _facture.LignesFacture = new List<LigneFacture>();
                        }
                        
                        _facture.LignesFacture.Add(nouvelleLigne);
                        
                        // Rafraîchir la grille des lignes
                        RefreshLignesFactureGrid();
                        
                        // Mettre en place une architecture évitant la mise à jour directe du stock dans le formulaire
                        // On laissera le repository s'occuper de la mise à jour du stock lors de l'ajout de la facture
                        
                        // Message de succès
                        MessageBox.Show($"Ligne ajoutée avec succès", 
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la vérification du stock: {ex.Message}", 
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la ligne: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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