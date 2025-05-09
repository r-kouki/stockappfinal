namespace StockApp.PieceForms
{
    partial class PieceDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.marqueLabel = new System.Windows.Forms.Label();
            this.marqueTextBox = new System.Windows.Forms.TextBox();
            this.referenceLabel = new System.Windows.Forms.Label();
            this.referenceTextBox = new System.Windows.Forms.TextBox();
            this.prixAchatLabel = new System.Windows.Forms.Label();
            this.prixAchatNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.prixVenteLabel = new System.Windows.Forms.Label();
            this.prixVenteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stockLabel = new System.Windows.Forms.Label();
            this.stockNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.seuilAlerteLabel = new System.Windows.Forms.Label();
            this.seuilAlerteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.tvaPctLabel = new System.Windows.Forms.Label();
            this.tvaPctNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.prixAchatNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prixVenteNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seuilAlerteNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvaPctNumericUpDown)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(400, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.AcceptButton = this.saveButton;
            this.CancelButton = this.cancelButton;
            
            // Labels & Controls setup
            int labelX = 20;
            int controlX = 150;
            int startY = 20;
            int heightStep = 30;
            int labelWidth = 120;
            int controlWidth = 200;
            int controlHeight = 23;
            
            // Marque
            this.marqueLabel.Location = new System.Drawing.Point(labelX, startY);
            this.marqueLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.marqueLabel.Text = "Marque:";
            this.marqueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.marqueTextBox.Location = new System.Drawing.Point(controlX, startY);
            this.marqueTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Reference
            this.referenceLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.referenceLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.referenceLabel.Text = "Référence:";
            this.referenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.referenceTextBox.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.referenceTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Prix Achat HT
            this.prixAchatLabel.Location = new System.Drawing.Point(labelX, startY + 2 * heightStep);
            this.prixAchatLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.prixAchatLabel.Text = "Prix Achat HT:";
            this.prixAchatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.prixAchatNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 2 * heightStep);
            this.prixAchatNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.prixAchatNumericUpDown.DecimalPlaces = 2;
            this.prixAchatNumericUpDown.Maximum = 10000;
            this.prixAchatNumericUpDown.Minimum = 0;
            
            // Prix Vente HT
            this.prixVenteLabel.Location = new System.Drawing.Point(labelX, startY + 3 * heightStep);
            this.prixVenteLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.prixVenteLabel.Text = "Prix Vente HT:";
            this.prixVenteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.prixVenteNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 3 * heightStep);
            this.prixVenteNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.prixVenteNumericUpDown.DecimalPlaces = 2;
            this.prixVenteNumericUpDown.Maximum = 10000;
            this.prixVenteNumericUpDown.Minimum = 0;
            
            // Stock
            this.stockLabel.Location = new System.Drawing.Point(labelX, startY + 4 * heightStep);
            this.stockLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.stockLabel.Text = "Stock:";
            this.stockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.stockNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 4 * heightStep);
            this.stockNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.stockNumericUpDown.Maximum = 1000;
            this.stockNumericUpDown.Minimum = 0;
            
            // Seuil Alerte
            this.seuilAlerteLabel.Location = new System.Drawing.Point(labelX, startY + 5 * heightStep);
            this.seuilAlerteLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.seuilAlerteLabel.Text = "Seuil Alerte:";
            this.seuilAlerteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.seuilAlerteNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 5 * heightStep);
            this.seuilAlerteNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.seuilAlerteNumericUpDown.Maximum = 100;
            this.seuilAlerteNumericUpDown.Minimum = 0;
            
            // TVA %
            this.tvaPctLabel.Location = new System.Drawing.Point(labelX, startY + 6 * heightStep);
            this.tvaPctLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.tvaPctLabel.Text = "TVA %:";
            this.tvaPctLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.tvaPctNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 6 * heightStep);
            this.tvaPctNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.tvaPctNumericUpDown.DecimalPlaces = 2;
            this.tvaPctNumericUpDown.Maximum = 100;
            this.tvaPctNumericUpDown.Minimum = 0;
            
            // Buttons
            this.saveButton.Location = new System.Drawing.Point(controlX - 50, startY + 8 * heightStep);
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.Text = "Sauvegarder";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            
            this.cancelButton.Location = new System.Drawing.Point(controlX + 60, startY + 8 * heightStep);
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.marqueLabel, this.marqueTextBox,
                this.referenceLabel, this.referenceTextBox,
                this.prixAchatLabel, this.prixAchatNumericUpDown,
                this.prixVenteLabel, this.prixVenteNumericUpDown,
                this.stockLabel, this.stockNumericUpDown,
                this.seuilAlerteLabel, this.seuilAlerteNumericUpDown,
                this.tvaPctLabel, this.tvaPctNumericUpDown,
                this.saveButton, this.cancelButton
            });
            
            ((System.ComponentModel.ISupportInitialize)(this.prixAchatNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prixVenteNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seuilAlerteNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvaPctNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label marqueLabel;
        private System.Windows.Forms.TextBox marqueTextBox;
        private System.Windows.Forms.Label referenceLabel;
        private System.Windows.Forms.TextBox referenceTextBox;
        private System.Windows.Forms.Label prixAchatLabel;
        private System.Windows.Forms.NumericUpDown prixAchatNumericUpDown;
        private System.Windows.Forms.Label prixVenteLabel;
        private System.Windows.Forms.NumericUpDown prixVenteNumericUpDown;
        private System.Windows.Forms.Label stockLabel;
        private System.Windows.Forms.NumericUpDown stockNumericUpDown;
        private System.Windows.Forms.Label seuilAlerteLabel;
        private System.Windows.Forms.NumericUpDown seuilAlerteNumericUpDown;
        private System.Windows.Forms.Label tvaPctLabel;
        private System.Windows.Forms.NumericUpDown tvaPctNumericUpDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
} 