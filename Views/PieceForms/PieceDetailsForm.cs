using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.PieceForms
{
    public partial class PieceDetailsForm : Form
    {
        private Piece _piece;
        private bool _isNewPiece;

        public PieceDetailsForm(Piece piece = null)
        {
            InitializeComponent();
            
            _piece = piece ?? new Piece { Id = Guid.NewGuid() };
            _isNewPiece = piece == null;
            
            // Set the form's title based on whether we're adding or editing
            this.Text = _isNewPiece ? "Ajouter une pièce" : "Modifier une pièce";
            
            // Load piece data if editing
            if (!_isNewPiece)
            {
                this.marqueTextBox.Text = _piece.Marque;
                this.referenceTextBox.Text = _piece.Reference;
                this.prixAchatNumericUpDown.Value = _piece.PrixAchatHT;
                this.prixVenteNumericUpDown.Value = _piece.PrixVenteHT;
                this.stockNumericUpDown.Value = _piece.Stock;
                this.seuilAlerteNumericUpDown.Value = _piece.SeuilAlerte;
                this.tvaPctNumericUpDown.Value = _piece.TvaPct;
            }
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(marqueTextBox.Text))
            {
                MessageBox.Show("La marque est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                marqueTextBox.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(referenceTextBox.Text))
            {
                MessageBox.Show("La référence est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                referenceTextBox.Focus();
                return;
            }
            
            // Update piece object with form values
            _piece.Marque = marqueTextBox.Text.Trim();
            _piece.Reference = referenceTextBox.Text.Trim();
            _piece.PrixAchatHT = prixAchatNumericUpDown.Value;
            _piece.PrixVenteHT = prixVenteNumericUpDown.Value;
            _piece.Stock = (int)stockNumericUpDown.Value;
            _piece.SeuilAlerte = (int)seuilAlerteNumericUpDown.Value;
            _piece.TvaPct = tvaPctNumericUpDown.Value;
            
            // Set DialogResult to indicate success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        // Property to access the piece data
        public Piece Piece => _piece;
    }
} 