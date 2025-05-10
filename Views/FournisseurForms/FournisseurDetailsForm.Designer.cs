namespace StockApp.FournisseurForms
{
    partial class FournisseurDetailsForm
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
            nomLabel = new Label();
            nomTextBox = new TextBox();
            prenomLabel = new Label();
            prenomTextBox = new TextBox();
            matFiscalLabel = new Label();
            matFiscalTextBox = new TextBox();
            adresseLabel = new Label();
            adresseTextBox = new TextBox();
            telephoneLabel = new Label();
            telephoneTextBox = new TextBox();
            creditLabel = new Label();
            creditNumericUpDown = new NumericUpDown();
            saveButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)creditNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // nomLabel
            // 
            nomLabel.Location = new Point(20, 9);
            nomLabel.Name = "nomLabel";
            nomLabel.Size = new Size(120, 23);
            nomLabel.TabIndex = 0;
            nomLabel.Text = "Nom:";
            nomLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nomTextBox
            // 
            nomTextBox.Location = new Point(150, 12);
            nomTextBox.Name = "nomTextBox";
            nomTextBox.Size = new Size(200, 27);
            nomTextBox.TabIndex = 1;
            // 
            // prenomLabel
            // 
            prenomLabel.Location = new Point(24, 45);
            prenomLabel.Name = "prenomLabel";
            prenomLabel.Size = new Size(120, 23);
            prenomLabel.TabIndex = 2;
            prenomLabel.Text = "Prénom:";
            prenomLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // prenomTextBox
            // 
            prenomTextBox.Location = new Point(150, 45);
            prenomTextBox.Name = "prenomTextBox";
            prenomTextBox.Size = new Size(200, 27);
            prenomTextBox.TabIndex = 3;
            // 
            // matFiscalLabel
            // 
            matFiscalLabel.Location = new Point(20, 76);
            matFiscalLabel.Name = "matFiscalLabel";
            matFiscalLabel.Size = new Size(120, 23);
            matFiscalLabel.TabIndex = 4;
            matFiscalLabel.Text = "Matricule Fiscal:";
            matFiscalLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // matFiscalTextBox
            // 
            matFiscalTextBox.Location = new Point(150, 78);
            matFiscalTextBox.Name = "matFiscalTextBox";
            matFiscalTextBox.Size = new Size(200, 27);
            matFiscalTextBox.TabIndex = 5;
            // 
            // adresseLabel
            // 
            adresseLabel.Location = new Point(20, 109);
            adresseLabel.Name = "adresseLabel";
            adresseLabel.Size = new Size(120, 23);
            adresseLabel.TabIndex = 6;
            adresseLabel.Text = "Adresse:";
            adresseLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // adresseTextBox
            // 
            adresseTextBox.Location = new Point(150, 107);
            adresseTextBox.Multiline = true;
            adresseTextBox.Name = "adresseTextBox";
            adresseTextBox.Size = new Size(200, 52);
            adresseTextBox.TabIndex = 7;
            // 
            // telephoneLabel
            // 
            telephoneLabel.Location = new Point(12, 167);
            telephoneLabel.Name = "telephoneLabel";
            telephoneLabel.Size = new Size(120, 23);
            telephoneLabel.TabIndex = 8;
            telephoneLabel.Text = "Téléphone:";
            telephoneLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // telephoneTextBox
            // 
            telephoneTextBox.Location = new Point(150, 165);
            telephoneTextBox.Name = "telephoneTextBox";
            telephoneTextBox.Size = new Size(200, 27);
            telephoneTextBox.TabIndex = 9;
            // 
            // creditLabel
            // 
            creditLabel.Location = new Point(12, 202);
            creditLabel.Name = "creditLabel";
            creditLabel.Size = new Size(120, 23);
            creditLabel.TabIndex = 10;
            creditLabel.Text = "Crédit:";
            creditLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // creditNumericUpDown
            // 
            creditNumericUpDown.DecimalPlaces = 2;
            creditNumericUpDown.Location = new Point(150, 198);
            creditNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            creditNumericUpDown.Name = "creditNumericUpDown";
            creditNumericUpDown.Size = new Size(200, 27);
            creditNumericUpDown.TabIndex = 11;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(84, 261);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 12;
            saveButton.Text = "Sauvegarder";
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(200, 261);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 30);
            cancelButton.TabIndex = 13;
            cancelButton.Text = "Annuler";
            cancelButton.Click += CancelButton_Click;
            // 
            // FournisseurDetailsForm
            // 
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            ClientSize = new Size(382, 303);
            Controls.Add(nomLabel);
            Controls.Add(nomTextBox);
            Controls.Add(prenomLabel);
            Controls.Add(prenomTextBox);
            Controls.Add(matFiscalLabel);
            Controls.Add(matFiscalTextBox);
            Controls.Add(adresseLabel);
            Controls.Add(adresseTextBox);
            Controls.Add(telephoneLabel);
            Controls.Add(telephoneTextBox);
            Controls.Add(creditLabel);
            Controls.Add(creditNumericUpDown);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FournisseurDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)creditNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
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