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
            this.pieceLabel = new System.Windows.Forms.Label();
            this.pieceComboBox = new System.Windows.Forms.ComboBox();
            this.quantiteLabel = new System.Windows.Forms.Label();
            this.quantiteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.prixUnitaireLabel = new System.Windows.Forms.Label();
            this.prixUnitaireNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.remiseLabel = new System.Windows.Forms.Label();
            this.remiseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.totalLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.quantiteNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prixUnitaireNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remiseNumericUpDown)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(450, 250);
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
            int controlWidth = 250;
            int controlHeight = 23;
            
            // Pièce
            this.pieceLabel.Location = new System.Drawing.Point(labelX, startY);
            this.pieceLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.pieceLabel.Text = "Pièce:";
            this.pieceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.pieceComboBox.Location = new System.Drawing.Point(controlX, startY);
            this.pieceComboBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.pieceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pieceComboBox.SelectedIndexChanged += new System.EventHandler(this.PieceComboBox_SelectedIndexChanged);
            
            // Quantité
            this.quantiteLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.quantiteLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.quantiteLabel.Text = "Quantité:";
            this.quantiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.quantiteNumericUpDown.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.quantiteNumericUpDown.Size = new System.Drawing.Size(100, controlHeight);
            this.quantiteNumericUpDown.Minimum = 1;
            this.quantiteNumericUpDown.Maximum = 1000;
            this.quantiteNumericUpDown.Value = 1;
            this.quantiteNumericUpDown.ValueChanged += new System.EventHandler(this.QuantiteNumericUpDown_ValueChanged);
            
            // Prix unitaire
            this.prixUnitaireLabel.Location = new System.Drawing.Point(labelX, startY + 2 * heightStep);
            this.prixUnitaireLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.prixUnitaireLabel.Text = "Prix unitaire HT:";
            this.prixUnitaireLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.prixUnitaireNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 2 * heightStep);
            this.prixUnitaireNumericUpDown.Size = new System.Drawing.Size(120, controlHeight);
            this.prixUnitaireNumericUpDown.DecimalPlaces = 2;
            this.prixUnitaireNumericUpDown.Minimum = 0;
            this.prixUnitaireNumericUpDown.Maximum = 10000;
            this.prixUnitaireNumericUpDown.Increment = 0.1M;
            this.prixUnitaireNumericUpDown.Value = 0;
            this.prixUnitaireNumericUpDown.ValueChanged += new System.EventHandler(this.PrixUnitaireNumericUpDown_ValueChanged);
            
            // Remise
            this.remiseLabel.Location = new System.Drawing.Point(labelX, startY + 3 * heightStep);
            this.remiseLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.remiseLabel.Text = "Remise (%):";
            this.remiseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.remiseNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 3 * heightStep);
            this.remiseNumericUpDown.Size = new System.Drawing.Size(80, controlHeight);
            this.remiseNumericUpDown.DecimalPlaces = 1;
            this.remiseNumericUpDown.Minimum = 0;
            this.remiseNumericUpDown.Maximum = 100;
            this.remiseNumericUpDown.Value = 0;
            this.remiseNumericUpDown.ValueChanged += new System.EventHandler(this.RemiseNumericUpDown_ValueChanged);
            
            // Total
            this.totalLabel.Location = new System.Drawing.Point(labelX, startY + 4 * heightStep);
            this.totalLabel.Size = new System.Drawing.Size(controlWidth + labelWidth, controlHeight);
            this.totalLabel.Text = "Total: 0.00 €";
            this.totalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            
            // Buttons
            this.saveButton.Location = new System.Drawing.Point(150, startY + 5 * heightStep + 10);
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.Text = "Ajouter";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            
            this.cancelButton.Location = new System.Drawing.Point(260, startY + 5 * heightStep + 10);
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pieceLabel, this.pieceComboBox,
                this.quantiteLabel, this.quantiteNumericUpDown,
                this.prixUnitaireLabel, this.prixUnitaireNumericUpDown,
                this.remiseLabel, this.remiseNumericUpDown,
                this.totalLabel,
                this.saveButton, this.cancelButton
            });
            
            ((System.ComponentModel.ISupportInitialize)(this.quantiteNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prixUnitaireNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remiseNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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