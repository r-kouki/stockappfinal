using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.FournisseurForms
{
    public partial class FournisseurDetailsForm : Form
    {
        private Fournisseur _fournisseur;
        private bool _isNewFournisseur;

        public FournisseurDetailsForm(Fournisseur fournisseur = null)
        {
            InitializeComponent();
            
            _fournisseur = fournisseur ?? new Fournisseur { Id = Guid.NewGuid() };
            _isNewFournisseur = fournisseur == null;
            
            // Set the form's title based on whether we're adding or editing
            this.Text = _isNewFournisseur ? "Ajouter un fournisseur" : "Modifier un fournisseur";
            
            // Load fournisseur data if editing
            if (!_isNewFournisseur)
            {
                this.nomTextBox.Text = _fournisseur.Nom;
                this.prenomTextBox.Text = _fournisseur.Prenom;
                this.matFiscalTextBox.Text = _fournisseur.MatFiscal;
                this.adresseTextBox.Text = _fournisseur.Adresse;
                this.telephoneTextBox.Text = _fournisseur.Telephone;
                this.creditNumericUpDown.Value = _fournisseur.Credit;
            }
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(nomTextBox.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nomTextBox.Focus();
                return;
            }
            
            // Update fournisseur object with form values
            _fournisseur.Nom = nomTextBox.Text.Trim();
            _fournisseur.Prenom = prenomTextBox.Text.Trim();
            _fournisseur.MatFiscal = matFiscalTextBox.Text.Trim();
            _fournisseur.Adresse = adresseTextBox.Text.Trim();
            _fournisseur.Telephone = telephoneTextBox.Text.Trim();
            _fournisseur.Credit = creditNumericUpDown.Value;
            
            // Set DialogResult to indicate success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        // Property to access the fournisseur data
        public Fournisseur Fournisseur => _fournisseur;
    }
} 