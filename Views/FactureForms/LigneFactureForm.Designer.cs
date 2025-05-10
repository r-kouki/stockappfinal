namespace StockApp.FactureForms
{
    partial class LigneFactureForm
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
            pieceLabel = new Label();
            pieceComboBox = new ComboBox();
            quantiteLabel = new Label();
            quantiteNumericUpDown = new NumericUpDown();
            prixUnitaireLabel = new Label();
            prixUnitaireNumericUpDown = new NumericUpDown();
            remiseLabel = new Label();
            remiseNumericUpDown = new NumericUpDown();
            totalLabel = new Label();
            saveButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)quantiteNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prixUnitaireNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)remiseNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // pieceLabel
            // 
            pieceLabel.Location = new Point(20, 23);
            pieceLabel.Name = "pieceLabel";
            pieceLabel.Size = new Size(120, 23);
            pieceLabel.TabIndex = 0;
            pieceLabel.Text = "Pièce:";
            pieceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pieceComboBox
            // 
            pieceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pieceComboBox.Location = new Point(150, 23);
            pieceComboBox.Name = "pieceComboBox";
            pieceComboBox.Size = new Size(250, 23);
            pieceComboBox.TabIndex = 1;
            pieceComboBox.SelectedIndexChanged += PieceComboBox_SelectedIndexChanged;
            // 
            // quantiteLabel
            // 
            quantiteLabel.Location = new Point(20, 50);
            quantiteLabel.Name = "quantiteLabel";
            quantiteLabel.Size = new Size(120, 23);
            quantiteLabel.TabIndex = 2;
            quantiteLabel.Text = "Quantité:";
            quantiteLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // quantiteNumericUpDown
            // 
            quantiteNumericUpDown.Location = new Point(150, 52);
            quantiteNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            quantiteNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            quantiteNumericUpDown.Name = "quantiteNumericUpDown";
            quantiteNumericUpDown.Size = new Size(100, 23);
            quantiteNumericUpDown.TabIndex = 3;
            quantiteNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            quantiteNumericUpDown.ValueChanged += QuantiteNumericUpDown_ValueChanged;
            // 
            // prixUnitaireLabel
            // 
            prixUnitaireLabel.Location = new Point(24, 79);
            prixUnitaireLabel.Name = "prixUnitaireLabel";
            prixUnitaireLabel.Size = new Size(120, 23);
            prixUnitaireLabel.TabIndex = 4;
            prixUnitaireLabel.Text = "Prix unitaire HT:";
            prixUnitaireLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // prixUnitaireNumericUpDown
            // 
            prixUnitaireNumericUpDown.DecimalPlaces = 2;
            prixUnitaireNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            prixUnitaireNumericUpDown.Location = new Point(150, 81);
            prixUnitaireNumericUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            prixUnitaireNumericUpDown.Name = "prixUnitaireNumericUpDown";
            prixUnitaireNumericUpDown.Size = new Size(120, 23);
            prixUnitaireNumericUpDown.TabIndex = 5;
            prixUnitaireNumericUpDown.ValueChanged += PrixUnitaireNumericUpDown_ValueChanged;
            // 
            // remiseLabel
            // 
            remiseLabel.Location = new Point(20, 110);
            remiseLabel.Name = "remiseLabel";
            remiseLabel.Size = new Size(120, 23);
            remiseLabel.TabIndex = 6;
            remiseLabel.Text = "Remise (%):";
            remiseLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // remiseNumericUpDown
            // 
            remiseNumericUpDown.DecimalPlaces = 1;
            remiseNumericUpDown.Location = new Point(150, 110);
            remiseNumericUpDown.Name = "remiseNumericUpDown";
            remiseNumericUpDown.Size = new Size(80, 23);
            remiseNumericUpDown.TabIndex = 7;
            remiseNumericUpDown.ValueChanged += RemiseNumericUpDown_ValueChanged;
            // 
            // totalLabel
            // 
            totalLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            totalLabel.Location = new Point(20, 143);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(250, 23);
            totalLabel.TabIndex = 8;
            totalLabel.Text = "Total: 0.00 €";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(150, 169);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 9;
            saveButton.Text = "Ajouter";
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(256, 169);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 30);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "Annuler";
            cancelButton.Click += CancelButton_Click;
            // 
            // LigneFactureForm
            // 
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            ClientSize = new Size(434, 211);
            Controls.Add(pieceLabel);
            Controls.Add(pieceComboBox);
            Controls.Add(quantiteLabel);
            Controls.Add(quantiteNumericUpDown);
            Controls.Add(prixUnitaireLabel);
            Controls.Add(prixUnitaireNumericUpDown);
            Controls.Add(remiseLabel);
            Controls.Add(remiseNumericUpDown);
            Controls.Add(totalLabel);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LigneFactureForm";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)quantiteNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)prixUnitaireNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)remiseNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label pieceLabel;
        private System.Windows.Forms.ComboBox pieceComboBox;
        private System.Windows.Forms.Label quantiteLabel;
        private System.Windows.Forms.NumericUpDown quantiteNumericUpDown;
        private System.Windows.Forms.Label prixUnitaireLabel;
        private System.Windows.Forms.NumericUpDown prixUnitaireNumericUpDown;
        private System.Windows.Forms.Label remiseLabel;
        private System.Windows.Forms.NumericUpDown remiseNumericUpDown;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
} 