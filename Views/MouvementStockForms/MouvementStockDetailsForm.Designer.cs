namespace StockApp.MouvementStockForms
{
    partial class MouvementStockDetailsForm
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
            this.dateLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.quantiteLabel = new System.Windows.Forms.Label();
            this.quantiteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pieceIdLabel = new System.Windows.Forms.Label();
            this.pieceIdTextBox = new System.Windows.Forms.TextBox();
            this.factureIdLabel = new System.Windows.Forms.Label();
            this.factureIdTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.quantiteNumericUpDown)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(400, 300);
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
            
            // Date
            this.dateLabel.Location = new System.Drawing.Point(labelX, startY);
            this.dateLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.dateLabel.Text = "Date:";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.dateTimePicker.Location = new System.Drawing.Point(controlX, startY);
            this.dateTimePicker.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            
            // Type
            this.typeLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.typeLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.typeLabel.Text = "Type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.typeComboBox.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.typeComboBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            // Quantité
            this.quantiteLabel.Location = new System.Drawing.Point(labelX, startY + 2 * heightStep);
            this.quantiteLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.quantiteLabel.Text = "Quantité:";
            this.quantiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.quantiteNumericUpDown.Location = new System.Drawing.Point(controlX, startY + 2 * heightStep);
            this.quantiteNumericUpDown.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.quantiteNumericUpDown.Minimum = -1000;
            this.quantiteNumericUpDown.Maximum = 1000;
            
            // Pièce ID
            this.pieceIdLabel.Location = new System.Drawing.Point(labelX, startY + 3 * heightStep);
            this.pieceIdLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.pieceIdLabel.Text = "Pièce ID:";
            this.pieceIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.pieceIdTextBox.Location = new System.Drawing.Point(controlX, startY + 3 * heightStep);
            this.pieceIdTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Facture ID
            this.factureIdLabel.Location = new System.Drawing.Point(labelX, startY + 4 * heightStep);
            this.factureIdLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.factureIdLabel.Text = "Facture ID:";
            this.factureIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.factureIdTextBox.Location = new System.Drawing.Point(controlX, startY + 4 * heightStep);
            this.factureIdTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Buttons
            this.saveButton.Location = new System.Drawing.Point(100, startY + 6 * heightStep);
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.Text = "Sauvegarder";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            
            this.cancelButton.Location = new System.Drawing.Point(210, startY + 6 * heightStep);
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dateLabel, this.dateTimePicker,
                this.typeLabel, this.typeComboBox,
                this.quantiteLabel, this.quantiteNumericUpDown,
                this.pieceIdLabel, this.pieceIdTextBox,
                this.factureIdLabel, this.factureIdTextBox,
                this.saveButton, this.cancelButton
            });
            
            ((System.ComponentModel.ISupportInitialize)(this.quantiteNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label quantiteLabel;
        private System.Windows.Forms.NumericUpDown quantiteNumericUpDown;
        private System.Windows.Forms.Label pieceIdLabel;
        private System.Windows.Forms.TextBox pieceIdTextBox;
        private System.Windows.Forms.Label factureIdLabel;
        private System.Windows.Forms.TextBox factureIdTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
} 