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
            dateLabel = new Label();
            dateTimePicker = new DateTimePicker();
            typeLabel = new Label();
            typeComboBox = new ComboBox();
            quantiteLabel = new Label();
            quantiteNumericUpDown = new NumericUpDown();
            pieceIdLabel = new Label();
            pieceIdTextBox = new TextBox();
            factureIdLabel = new Label();
            factureIdTextBox = new TextBox();
            saveButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)quantiteNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // dateLabel
            // 
            dateLabel.Location = new Point(20, 14);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(120, 23);
            dateLabel.TabIndex = 0;
            dateLabel.Text = "Date:";
            dateLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(146, 12);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 1;
            // 
            // typeLabel
            // 
            typeLabel.Location = new Point(20, 43);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(120, 23);
            typeLabel.TabIndex = 2;
            typeLabel.Text = "Type:";
            typeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // typeComboBox
            // 
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeComboBox.Location = new Point(146, 44);
            typeComboBox.Name = "typeComboBox";
            typeComboBox.Size = new Size(200, 23);
            typeComboBox.TabIndex = 3;
            // 
            // quantiteLabel
            // 
            quantiteLabel.Location = new Point(24, 73);
            quantiteLabel.Name = "quantiteLabel";
            quantiteLabel.Size = new Size(120, 23);
            quantiteLabel.TabIndex = 4;
            quantiteLabel.Text = "Quantité:";
            quantiteLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // quantiteNumericUpDown
            // 
            quantiteNumericUpDown.Location = new Point(146, 73);
            quantiteNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            quantiteNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            quantiteNumericUpDown.Name = "quantiteNumericUpDown";
            quantiteNumericUpDown.Size = new Size(200, 23);
            quantiteNumericUpDown.TabIndex = 5;
            // 
            // pieceIdLabel
            // 
            pieceIdLabel.Location = new Point(20, 102);
            pieceIdLabel.Name = "pieceIdLabel";
            pieceIdLabel.Size = new Size(120, 23);
            pieceIdLabel.TabIndex = 6;
            pieceIdLabel.Text = "Pièce ID:";
            pieceIdLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pieceIdTextBox
            // 
            pieceIdTextBox.Location = new Point(146, 103);
            pieceIdTextBox.Name = "pieceIdTextBox";
            pieceIdTextBox.Size = new Size(200, 23);
            pieceIdTextBox.TabIndex = 7;
            // 
            // factureIdLabel
            // 
            factureIdLabel.Location = new Point(24, 132);
            factureIdLabel.Name = "factureIdLabel";
            factureIdLabel.Size = new Size(120, 23);
            factureIdLabel.TabIndex = 8;
            factureIdLabel.Text = "Facture ID:";
            factureIdLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // factureIdTextBox
            // 
            factureIdTextBox.Location = new Point(146, 133);
            factureIdTextBox.Name = "factureIdTextBox";
            factureIdTextBox.Size = new Size(200, 23);
            factureIdTextBox.TabIndex = 9;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(98, 183);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 10;
            saveButton.Text = "Sauvegarder";
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(214, 183);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 30);
            cancelButton.TabIndex = 11;
            cancelButton.Text = "Annuler";
            cancelButton.Click += CancelButton_Click;
            // 
            // MouvementStockDetailsForm
            // 
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            ClientSize = new Size(384, 227);
            Controls.Add(dateLabel);
            Controls.Add(dateTimePicker);
            Controls.Add(typeLabel);
            Controls.Add(typeComboBox);
            Controls.Add(quantiteLabel);
            Controls.Add(quantiteNumericUpDown);
            Controls.Add(pieceIdLabel);
            Controls.Add(pieceIdTextBox);
            Controls.Add(factureIdLabel);
            Controls.Add(factureIdTextBox);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MouvementStockDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)quantiteNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
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