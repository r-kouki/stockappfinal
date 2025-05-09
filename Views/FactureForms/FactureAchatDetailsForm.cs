using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.FactureForms
{
    public partial class FactureAchatDetailsForm : Form
    {
        private readonly IFournisseurRepository _fournisseurRepository;
        private FactureAchat _facture;
        private bool _isNewFacture;
        private List<Fournisseur> _fournisseurs;

        public FactureAchatDetailsForm(FactureAchat facture = null)
        {
            InitializeComponent();
            
            // Obtenir le repository depuis le conteneur DI
            _fournisseurRepository = Program.ServiceProvider.GetRequiredService<IFournisseurRepository>();
            
            _facture = facture ?? new FactureAchat { Id = Guid.NewGuid(), Date = DateTime.Now, LignesFacture = new List<LigneFacture>() };
            _isNewFacture = facture == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewFacture ? "Ajouter une facture d'achat" : "Modifier une facture d'achat";
            
            // Charger la liste des fournisseurs depuis la base de données
            LoadFournisseursAsync();
            
            // Charger les données de la facture si en mode édition
            if (!_isNewFacture)
            {
                this.dateTimePicker.Value = _facture.Date;
                
                // La sélection du fournisseur sera faite après le chargement des données
                
                // Afficher les lignes de facture s'il y en a
                if (_facture.LignesFacture != null && _facture.LignesFacture.Count > 0)
                {
                    RefreshLignesFactureGrid();
                }
            }
        }
        
        private async void LoadFournisseursAsync()
        {
            try
            {
                // Désactiver la ComboBox pendant le chargement
                fournisseurComboBox.Enabled = false;
                
                // Obtenir les fournisseurs depuis la base de données
                var fournisseurs = await _fournisseurRepository.GetAllAsync();
                _fournisseurs = new List<Fournisseur>(fournisseurs);
                
                // Mettre à jour la ComboBox
                fournisseurComboBox.DataSource = _fournisseurs;
                fournisseurComboBox.DisplayMember = "Nom";
                fournisseurComboBox.ValueMember = "Id";
                
                // Réactiver la ComboBox
                fournisseurComboBox.Enabled = true;
                
                // Sélectionner le fournisseur pour une facture existante
                if (!_isNewFacture)
                {
                    foreach (Fournisseur fournisseur in fournisseurComboBox.Items)
                    {
                        if (fournisseur.Id == _facture.FournisseurId)
                        {
                            fournisseurComboBox.SelectedItem = fournisseur;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des fournisseurs: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (fournisseurComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fournisseurComboBox.Focus();
                return;
            }
            
            // Validation supplémentaire...
            
            // Mettre à jour l'objet facture avec les valeurs du formulaire
            _facture.Date = dateTimePicker.Value;
            
            // Récupérer l'ID du fournisseur sélectionné
            if (fournisseurComboBox.SelectedItem is Fournisseur selectedFournisseur)
            {
                _facture.FournisseurId = selectedFournisseur.Id;
                _facture.Fournisseur = selectedFournisseur;
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
                
                // Créer un mouvement de stock (entrée) associé
                var mouvement = new MouvementStock
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Type = "ENTREE",
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
            
            // Créer une liste temporaire pour afficher les descriptions des pièces
            var lignesAffichage = new List<dynamic>();
            foreach (var ligne in _facture.LignesFacture)
            {
                // On devrait idéalement charger les pièces depuis la base de données
                // pour avoir des descriptions correctes ici
                lignesAffichage.Add(new 
                {
                    ligne.Piece,
                    ligne.Quantite,
                    ligne.PrixUnitaireHT,
                    ligne.RemisePct
                });
            }
            
            lignesDataGridView.DataSource = _facture.LignesFacture;
        }
        
        // Propriété pour accéder aux données de la facture
        public FactureAchat Facture => _facture;
    }
} 