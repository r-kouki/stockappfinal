using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.ClientForms
{
    public partial class ClientDetailsForm : Form
    {
        private Client _client;
        private bool _isNewClient;

        public ClientDetailsForm(Client client = null)
        {
            InitializeComponent();
            
            _client = client ?? new Client { Id = string.Empty };
            _isNewClient = client == null;
            
            // Set the form's title based on whether we're adding or editing
            this.Text = _isNewClient ? "Ajouter un client" : "Modifier un client";
            
            // Load client data if editing
            if (!_isNewClient)
            {
                this.nomTextBox.Text = _client.Nom;
                this.prenomTextBox.Text = _client.Prenom;
                this.matFiscalTextBox.Text = _client.MatFiscal;
                this.adresseTextBox.Text = _client.Adresse;
                this.telephoneTextBox.Text = _client.Telephone;
                this.creditNumericUpDown.Value = _client.Credit;
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
            
            // Update client object with form values
            _client.Nom = nomTextBox.Text.Trim();
            _client.Prenom = prenomTextBox.Text.Trim();
            _client.MatFiscal = matFiscalTextBox.Text.Trim();
            _client.Adresse = adresseTextBox.Text.Trim();
            _client.Telephone = telephoneTextBox.Text.Trim();
            _client.Credit = creditNumericUpDown.Value;
            
            // Set DialogResult to indicate success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        // Property to access the client data
        public Client Client => _client;
    }
} 