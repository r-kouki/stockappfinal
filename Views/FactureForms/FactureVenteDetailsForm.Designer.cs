namespace StockApp.FactureForms
{
    partial class FactureVenteDetailsForm
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
            clientLabel = new Label();
            clientComboBox = new ComboBox();
            lignesGroupBox = new GroupBox();
            lignesDataGridView = new DataGridView();
            addLigneButton = new Button();
            saveButton = new Button();
            cancelButton = new Button();
            dateEcheanceLabel = new Label();
            dateEcheanceTimePicker = new DateTimePicker();
            lignesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lignesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // dateLabel
            // 
            dateLabel.Location = new Point(61, 16);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(120, 23);
            dateLabel.TabIndex = 0;
            dateLabel.Text = "Date:";
            dateLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(200, 12);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 27);
            dateTimePicker.TabIndex = 1;
            // 
            // clientLabel
            // 
            clientLabel.Location = new Point(61, 45);
            clientLabel.Name = "clientLabel";
            clientLabel.Size = new Size(120, 23);
            clientLabel.TabIndex = 2;
            clientLabel.Text = "Client:";
            clientLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // clientComboBox
            // 
            clientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            clientComboBox.Location = new Point(200, 45);
            clientComboBox.Name = "clientComboBox";
            clientComboBox.Size = new Size(200, 28);
            clientComboBox.TabIndex = 3;
            // 
            // dateEcheanceLabel
            // 
            dateEcheanceLabel.Location = new Point(61, 75);
            dateEcheanceLabel.Name = "dateEcheanceLabel";
            dateEcheanceLabel.Size = new Size(120, 23);
            dateEcheanceLabel.TabIndex = 7;
            dateEcheanceLabel.Text = "Date d'échéance:";
            dateEcheanceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dateEcheanceTimePicker
            // 
            dateEcheanceTimePicker.Format = DateTimePickerFormat.Short;
            dateEcheanceTimePicker.Location = new Point(200, 75);
            dateEcheanceTimePicker.Name = "dateEcheanceTimePicker";
            dateEcheanceTimePicker.Size = new Size(200, 27);
            dateEcheanceTimePicker.TabIndex = 8;
            // 
            // lignesGroupBox
            // 
            lignesGroupBox.Controls.Add(lignesDataGridView);
            lignesGroupBox.Controls.Add(addLigneButton);
            lignesGroupBox.Location = new Point(20, 105);
            lignesGroupBox.Name = "lignesGroupBox";
            lignesGroupBox.Size = new Size(550, 280);
            lignesGroupBox.TabIndex = 4;
            lignesGroupBox.TabStop = false;
            lignesGroupBox.Text = "Lignes de facture";
            // 
            // lignesDataGridView
            // 
            lignesDataGridView.AllowUserToAddRows = false;
            lignesDataGridView.AllowUserToDeleteRows = false;
            lignesDataGridView.ColumnHeadersHeight = 29;
            lignesDataGridView.Dock = DockStyle.Top;
            lignesDataGridView.Location = new Point(3, 23);
            lignesDataGridView.MultiSelect = false;
            lignesDataGridView.Name = "lignesDataGridView";
            lignesDataGridView.ReadOnly = true;
            lignesDataGridView.RowHeadersWidth = 51;
            lignesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lignesDataGridView.Size = new Size(544, 210);
            lignesDataGridView.TabIndex = 0;
            // 
            // addLigneButton
            // 
            addLigneButton.Location = new Point(196, 239);
            addLigneButton.Name = "addLigneButton";
            addLigneButton.Size = new Size(153, 30);
            addLigneButton.TabIndex = 1;
            addLigneButton.Text = "Ajouter ligne";
            addLigneButton.Click += AddLigneButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(200, 420);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.TabIndex = 5;
            saveButton.Text = "Sauvegarder";
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(310, 420);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 30);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Annuler";
            cancelButton.Click += CancelButton_Click;
            // 
            // FactureVenteDetailsForm
            // 
            AcceptButton = saveButton;
            CancelButton = cancelButton;
            ClientSize = new Size(582, 453);
            Controls.Add(dateLabel);
            Controls.Add(clientLabel);
            Controls.Add(clientComboBox);
            Controls.Add(dateTimePicker);
            Controls.Add(dateEcheanceLabel);
            Controls.Add(dateEcheanceTimePicker);
            Controls.Add(lignesGroupBox);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FactureVenteDetailsForm";
            StartPosition = FormStartPosition.CenterParent;
            lignesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lignesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label clientLabel;
        private System.Windows.Forms.ComboBox clientComboBox;
        private System.Windows.Forms.GroupBox lignesGroupBox;
        private System.Windows.Forms.DataGridView lignesDataGridView;
        private System.Windows.Forms.Button addLigneButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label dateEcheanceLabel;
        private System.Windows.Forms.DateTimePicker dateEcheanceTimePicker;
    }
} 