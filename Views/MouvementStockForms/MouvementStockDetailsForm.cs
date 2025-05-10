using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.MouvementStockForms
{
    public partial class MouvementStockDetailsForm : Form
    {
        private MouvementStock _mouvement;
        
        public MouvementStock Mouvement => _mouvement;
        
        public MouvementStockDetailsForm(MouvementStock mouvement = null)
        {
            InitializeComponent();
            
            // Use the mouvement passed in or create a new one
            _mouvement = mouvement ?? new MouvementStock { Id = string.Empty, Date = DateTime.Now, Type = "ENTREE", Quantite = 0 };
            
            // Set up combobox items
            typeComboBox.Items.Add("ENTREE");
            typeComboBox.Items.Add("SORTIE");
            
            // Bind data to controls
            dateTimePicker.Value = _mouvement.Date;
            typeComboBox.SelectedItem = _mouvement.Type;
            quantiteNumericUpDown.Value = _mouvement.Quantite;
            
            if (_mouvement.PieceId != null)
            {
                pieceIdTextBox.Text = _mouvement.PieceId;
            }
            
            if (_mouvement.FactureId != null)
            {
                factureIdTextBox.Text = _mouvement.FactureId;
            }
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
            
            // Set the piece ID
            _mouvement.PieceId = pieceIdTextBox.Text;
            
            // Set the facture ID if provided
            if (!string.IsNullOrWhiteSpace(factureIdTextBox.Text))
            {
                _mouvement.FactureId = factureIdTextBox.Text;
            }
            else
            {
                _mouvement.FactureId = null;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 
 