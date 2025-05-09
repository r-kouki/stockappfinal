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
    public partial class FactureAchatDetailsForm : Form
    {
        private readonly IFournisseurRepository _fournisseurRepository;
        private readonly IMouvementStockRepository _mouvementStockRepository;
        private readonly IPieceRepository _pieceRepository;
        private FactureAchat _facture;
        private bool _isNewFacture;
        private List<Fournisseur> _fournisseurs;

        public FactureAchatDetailsForm(FactureAchat facture = null)
        {
            InitializeComponent();
            
            // Obtenir les repositories depuis le conteneur DI
            _fournisseurRepository = Program.ServiceProvider.GetRequiredService<IFournisseurRepository>();
            _mouvementStockRepository = Program.ServiceProvider.GetRequiredService<IMouvementStockRepository>();
            _pieceRepository = Program.ServiceProvider.GetRequiredService<IPieceRepository>();
            
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
        
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (fournisseurComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fournisseurComboBox.Focus();
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
                    if (ligneForm.LigneFacture == null || ligneForm.LigneFacture.PieceId == Guid.Empty)
                    {
                        MessageBox.Show("Données de ligne invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Ajouter la ligne à la facture
                    if (_facture.LignesFacture == null)
                    {
                        _facture.LignesFacture = new List<LigneFacture>();
                    }
                    
                    _facture.LignesFacture.Add(ligneForm.LigneFacture);
                    
                    // Lier la ligne à cette facture
                    ligneForm.LigneFacture.FactureId = _facture.Id;
                    ligneForm.LigneFacture.Facture = _facture;
                    
                    // Rafraîchir la grille des lignes
                    RefreshLignesFactureGrid();
                    
                    try
                    {
                        // Obtenir la pièce correspondante
                        var piece = await _pieceRepository.GetByIdAsync(ligneForm.LigneFacture.PieceId);
                        if (piece != null)
                        {
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
                            
                            // Enregistrer le mouvement dans la base de données
                            await _mouvementStockRepository.AddAsync(mouvement);
                            
                            // Mettre à jour le stock de la pièce
                            int nouvelleQuantite = piece.Stock + ligneForm.LigneFacture.Quantite;
                            
                            // Utiliser une approche différente pour mettre à jour le stock
                            // En créant un contexte séparé pour cette opération
                            using (var scope = Program.ServiceProvider.CreateScope())
                            {
                                var stockContext = scope.ServiceProvider.GetRequiredService<StockContext>();
                                var pieceToUpdate = await stockContext.Pieces.FindAsync(piece.Id);
                                if (pieceToUpdate != null)
                                {
                                    pieceToUpdate.Stock = nouvelleQuantite;
                                    await stockContext.SaveChangesAsync();
                                }
                            }
                            
                            MessageBox.Show($"Ligne ajoutée et stock mis à jour (+{ligneForm.LigneFacture.Quantite})", 
                                "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Si la pièce n'est pas trouvée, ajouter juste la ligne sans mettre à jour le stock
                            MessageBox.Show("Ligne ajoutée. Attention: impossible de mettre à jour le stock car la pièce n'a pas été trouvée dans la base de données.", 
                                "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // La ligne a été ajoutée mais il y a eu une erreur avec le mouvement de stock
                        MessageBox.Show($"Ligne ajoutée mais erreur lors de la mise à jour du stock: {ex.Message}\n{ex.InnerException?.Message}", 
                            "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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