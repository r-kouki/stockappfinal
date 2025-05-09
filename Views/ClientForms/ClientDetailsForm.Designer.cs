namespace StockApp.ClientForms
{
    partial class ClientDetailsForm
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
            this.nomLabel = new System.Windows.Forms.Label();
            this.nomTextBox = new System.Windows.Forms.TextBox();
            this.prenomLabel = new System.Windows.Forms.Label();
            this.prenomTextBox = new System.Windows.Forms.TextBox();
            this.matFiscalLabel = new System.Windows.Forms.Label();
            this.matFiscalTextBox = new System.Windows.Forms.TextBox();
            this.adresseLabel = new System.Windows.Forms.Label();
            this.adresseTextBox = new System.Windows.Forms.TextBox();
            this.telephoneLabel = new System.Windows.Forms.Label();
            this.telephoneTextBox = new System.Windows.Forms.TextBox();
            this.creditLabel = new System.Windows.Forms.Label();
            this.creditNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.creditNumericUpDown)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(400, 350);
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
            
            // Nom
            this.nomLabel.Location = new System.Drawing.Point(labelX, startY);
            this.nomLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.nomLabel.Text = "Nom:";
            this.nomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.nomTextBox.Location = new System.Drawing.Point(controlX, startY);
            this.nomTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Prenom
            this.prenomLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.prenomLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.prenomLabel.Text = "Prénom:";
            this.prenomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.prenomTextBox.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.prenomTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // MatFiscal
            this.matFiscalLabel.Location = new System.Drawing.Point(labelX, startY + 2 * heightStep);
            this.matFiscalLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.matFiscalLabel.Text = "Matricule Fiscal:";
            this.matFiscalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.matFiscalTextBox.Location = new System.Drawing.Point(controlX, startY + 2 * heightStep);
            this.matFiscalTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Adresse
            this.adresseLabel.Location = new System.Drawing.Point(labelX, startY + 3 * heightStep);
            this.adresseLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.adresseLabel.Text = "Adresse:";
            this.adresseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.adresseTextBox.Location = new System.Drawing.Point(controlX, startY + 3 * heightStep);
            this.adresseTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight * 2);
            this.adresseTextBox.Multiline = true;
            
            // Telephone
            this.telephoneLabel.Location = new System.Drawing.Point(labelX, startY + 5 * heightStep);
            this.telephoneLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.telephoneLabel.Text = "Téléphone:";
            this.telephoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.telephoneTextBox.Location = new System.Drawing.Point(controlX, startY + 5 * heightStep);
            this.telephoneTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Credit
            this.creditLabel.Location = new System.Drawing.Point(labelX, startY + 6 * heightStep);
            this.creditLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.creditLabel.Text = "Crédit:";
            this.creditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.creditNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 6 * heightStep);
            this.creditNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.creditNumericUpDown.DecimalPlaces = 2;
            this.creditNumericUpDown.Maximum = 1000000;
            this.creditNumericUpDown.Minimum = 0;
            
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
                this.nomLabel, this.nomTextBox,
                this.prenomLabel, this.prenomTextBox,
                this.matFiscalLabel, this.matFiscalTextBox,
                this.adresseLabel, this.adresseTextBox,
                this.telephoneLabel, this.telephoneTextBox,
                this.creditLabel, this.creditNumericUpDown,
                this.saveButton, this.cancelButton
            });
            
            ((System.ComponentModel.ISupportInitialize)(this.creditNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label nomLabel;
        private System.Windows.Forms.TextBox nomTextBox;
        private System.Windows.Forms.Label prenomLabel;
        private System.Windows.Forms.TextBox prenomTextBox;
        private System.Windows.Forms.Label matFiscalLabel;
        private System.Windows.Forms.TextBox matFiscalTextBox;
        private System.Windows.Forms.Label adresseLabel;
        private System.Windows.Forms.TextBox adresseTextBox;
        private System.Windows.Forms.Label telephoneLabel;
        private System.Windows.Forms.TextBox telephoneTextBox;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.NumericUpDown creditNumericUpDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
} 