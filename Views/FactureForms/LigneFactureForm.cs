using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FactureForms
{
    public partial class LigneFactureForm : Form
    {
        private readonly IPieceRepository _pieceRepository;
        private LigneFacture _ligneFacture;
        private List<Piece> _piecesList;
        private bool _isNewLigne;
        private bool _dataLoaded = false;

        public LigneFactureForm(LigneFacture ligneFacture = null)
        {
            InitializeComponent();
            
            try
            {
                // Obtenir le repository depuis le conteneur DI
                _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
                
                _ligneFacture = ligneFacture ?? new LigneFacture { Id = string.Empty };
                _isNewLigne = ligneFacture == null;
                
                // Configuration du titre du formulaire
                this.Text = _isNewLigne ? "Ajouter une ligne" : "Modifier une ligne";
                
                // Désactiver le bouton de sauvegarde jusqu'à ce que les données soient chargées
                this.saveButton.Enabled = false;
                
                // Charger les pièces depuis la base de données
                LoadPiecesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void LoadPiecesAsync()
        {
            try
            {
                // Désactiver la ComboBox pendant le chargement
                pieceComboBox.Enabled = false;
                
                // Obtenir les pièces depuis la base de données
                var pieces = await _pieceRepository.GetAllAsync();
                _piecesList = new List<Piece>(pieces);
                
                if (_piecesList.Count == 0)
                {
                    MessageBox.Show("Aucune pièce trouvée dans la base de données. Veuillez d'abord ajouter des pièces.", 
                        "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Mettre à jour la ComboBox
                pieceComboBox.DataSource = null;
                pieceComboBox.DataSource = _piecesList;
                pieceComboBox.DisplayMember = "Reference";
                pieceComboBox.ValueMember = "Id";
                
                // Réactiver la ComboBox
                pieceComboBox.Enabled = true;
                
                // Activer le bouton de sauvegarde
                saveButton.Enabled = true;
                
                // Charger les données de la ligne si en mode édition
                if (!_isNewLigne)
                {
                    quantiteNumericUpDown.Value = _ligneFacture.Quantite;
                    prixUnitaireNumericUpDown.Value = _ligneFacture.PrixUnitaireHT;
                    remiseNumericUpDown.Value = _ligneFacture.RemisePct;
                    
                    // Sélectionner la pièce
                    foreach (Piece piece in pieceComboBox.Items)
                    {
                        if (piece.Id == _ligneFacture.PieceId)
                        {
                            pieceComboBox.SelectedItem = piece;
                            break;
                        }
                    }
                }
                else if (pieceComboBox.Items.Count > 0)
                {
                    // Sélectionner la première pièce par défaut
                    pieceComboBox.SelectedIndex = 0;
                }
                
                _dataLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des pièces: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        
        private void PieceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_dataLoaded) return;
            
            if (pieceComboBox.SelectedItem is Piece selectedPiece)
            {
                try
                {
                    // Mettre à jour le prix unitaire par défaut en fonction de la pièce sélectionnée
                    prixUnitaireNumericUpDown.Value = selectedPiece.PrixVenteHT;
                    
                    // Mettre à jour l'affichage du total
                    CalculerEtAfficherTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la sélection de la pièce: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void QuantiteNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CalculerEtAfficherTotal();
        }
        
        private void PrixUnitaireNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CalculerEtAfficherTotal();
        }
        
        private void RemiseNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CalculerEtAfficherTotal();
        }
        
        private void CalculerEtAfficherTotal()
        {
            decimal quantite = quantiteNumericUpDown.Value;
            decimal prixUnitaire = prixUnitaireNumericUpDown.Value;
            decimal remisePct = remiseNumericUpDown.Value;
            
            decimal totalHT = quantite * prixUnitaire * (1 - remisePct / 100);
            
            // Récupérer le taux de TVA de la pièce sélectionnée
            decimal tvaPct = 0;
            if (pieceComboBox.SelectedItem is Piece selectedPiece)
            {
                tvaPct = selectedPiece.TvaPct;
            }
            
            decimal totalTTC = totalHT * (1 + tvaPct / 100);
            
            totalLabel.Text = $"Total HT: {totalHT:C2} - Total TTC: {totalTTC:C2}";
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (pieceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une pièce.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pieceComboBox.Focus();
                return;
            }
            
            if (quantiteNumericUpDown.Value <= 0)
            {
                MessageBox.Show("La quantité doit être supérieure à zéro.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                quantiteNumericUpDown.Focus();
                return;
            }
            
            try
            {
                // Créer un nouvel objet LigneFacture plutôt que de modifier l'existant
                // Cette approche évite les problèmes de suivi d'entité
                _ligneFacture = new LigneFacture
                {
                    Id = _isNewLigne ? string.Empty : _ligneFacture.Id, // Conserver l'ID si en édition
                    Quantite = (int)quantiteNumericUpDown.Value,
                    PrixUnitaireHT = prixUnitaireNumericUpDown.Value,
                    RemisePct = remiseNumericUpDown.Value,
                    FactureId = _ligneFacture.FactureId // Conserver la référence à la facture
                };
                
                // Récupérer seulement l'ID de la pièce sélectionnée sans conserver la référence à l'objet Piece
                if (pieceComboBox.SelectedItem is Piece selectedPiece)
                {
                    _ligneFacture.PieceId = selectedPiece.Id;
                    // Ne pas stocker la référence à l'objet Piece pour éviter les conflits de tracking
                }
                
                // Définir le DialogResult pour indiquer le succès
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement de la ligne: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        // Propriété pour accéder aux données de la ligne
        public LigneFacture LigneFacture => _ligneFacture;
    }
} 