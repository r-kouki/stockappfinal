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

        public LigneFactureForm(LigneFacture ligneFacture = null)
        {
            InitializeComponent();
            
            // Obtenir le repository depuis le conteneur DI
            _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
            
            _ligneFacture = ligneFacture ?? new LigneFacture { Id = Guid.NewGuid() };
            _isNewLigne = ligneFacture == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewLigne ? "Ajouter une ligne" : "Modifier une ligne";
            
            // Charger les pièces depuis la base de données
            LoadPiecesAsync();
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
                
                // Mettre à jour la ComboBox
                pieceComboBox.DataSource = _piecesList;
                pieceComboBox.DisplayMember = "Reference";
                pieceComboBox.ValueMember = "Id";
                
                // Réactiver la ComboBox
                pieceComboBox.Enabled = true;
                
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des pièces: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PieceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pieceComboBox.SelectedItem is Piece selectedPiece)
            {
                // Mettre à jour le prix unitaire par défaut en fonction de la pièce sélectionnée
                prixUnitaireNumericUpDown.Value = selectedPiece.PrixVenteHT;
                
                // Mettre à jour l'affichage du total
                CalculerEtAfficherTotal();
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
            
            // Mettre à jour l'objet ligne avec les valeurs du formulaire
            _ligneFacture.Quantite = (int)quantiteNumericUpDown.Value;
            _ligneFacture.PrixUnitaireHT = prixUnitaireNumericUpDown.Value;
            _ligneFacture.RemisePct = remiseNumericUpDown.Value;
            
            // Récupérer l'ID de la pièce sélectionnée
            if (pieceComboBox.SelectedItem is Piece selectedPiece)
            {
                _ligneFacture.PieceId = selectedPiece.Id;
                _ligneFacture.Piece = selectedPiece;
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
        
        // Propriété pour accéder aux données de la ligne
        public LigneFacture LigneFacture => _ligneFacture;
    }
} 