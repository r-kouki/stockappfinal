using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.MouvementStockForms
{
    public partial class MouvementStockDetailsForm : Form
    {
        private MouvementStock _mouvement;
        private bool _isNewMouvement;

        public MouvementStockDetailsForm(MouvementStock mouvement = null)
        {
            InitializeComponent();
            
            _mouvement = mouvement ?? new MouvementStock { Id = Guid.NewGuid(), Date = DateTime.Now, Type = "ENTREE", Quantite = 0 };
            _isNewMouvement = mouvement == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewMouvement ? "Ajouter un mouvement de stock" : "Modifier un mouvement de stock";
            
            // Remplir le combobox des types
            typeComboBox.Items.AddRange(new string[] { "ENTREE", "SORTIE", "AJUSTEMENT_INV", "RETOUR_CLIENT", "RETOUR_FOURNISSEUR" });
            
            // Charger les données du mouvement si en mode édition
            if (!_isNewMouvement)
            {
                this.dateTimePicker.Value = _mouvement.Date;
                this.typeComboBox.SelectedItem = _mouvement.Type;
                this.quantiteNumericUpDown.Value = _mouvement.Quantite;
                this.pieceIdTextBox.Text = _mouvement.PieceId.ToString();
                
                if (_mouvement.FactureId.HasValue)
                {
                    this.factureIdTextBox.Text = _mouvement.FactureId.Value.ToString();
                }
            }
            else
            {
                this.typeComboBox.SelectedIndex = 0; // Par défaut "ENTREE"
            }
            
            // TODO: Dans une vraie implémentation, vous devriez charger la liste des pièces
            // dans un combobox pour permettre à l'utilisateur de sélectionner une pièce.
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un type de mouvement.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeComboBox.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(pieceIdTextBox.Text))
            {
                MessageBox.Show("Veuillez sélectionner une pièce.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pieceIdTextBox.Focus();
                return;
            }
            
            // Validation supplémentaire...
            
            // Mettre à jour l'objet mouvement avec les valeurs du formulaire
            _mouvement.Date = dateTimePicker.Value;
            _mouvement.Type = typeComboBox.SelectedItem.ToString();
            _mouvement.Quantite = (int)quantiteNumericUpDown.Value;
            
            // Essayer de parser le Guid de la pièce
            if (Guid.TryParse(pieceIdTextBox.Text, out Guid pieceId))
            {
                _mouvement.PieceId = pieceId;
            }
            else
            {
                MessageBox.Show("ID de pièce invalide.", "Erreur de validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Essayer de parser le Guid de la facture si fourni
            if (!string.IsNullOrWhiteSpace(factureIdTextBox.Text))
            {
                if (Guid.TryParse(factureIdTextBox.Text, out Guid factureId))
                {
                    _mouvement.FactureId = factureId;
                }
                else
                {
                    MessageBox.Show("ID de facture invalide.", "Erreur de validation", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                _mouvement.FactureId = null;
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
        
        // Propriété pour accéder aux données du mouvement
        public MouvementStock Mouvement => _mouvement;
    }
} 
 