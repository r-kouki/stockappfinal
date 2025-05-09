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
            this.dateLabel = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.clientLabel = new System.Windows.Forms.Label();
            this.clientComboBox = new System.Windows.Forms.ComboBox();
            this.lignesGroupBox = new System.Windows.Forms.GroupBox();
            this.lignesDataGridView = new System.Windows.Forms.DataGridView();
            this.addLigneButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            this.lignesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lignesDataGridView)).BeginInit();
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(600, 500);
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
            
            // Client
            this.clientLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.clientLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.clientLabel.Text = "Client:";
            this.clientLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.clientComboBox.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.clientComboBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.clientComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            // Lignes de facture
            this.lignesGroupBox.Text = "Lignes de facture";
            this.lignesGroupBox.Location = new System.Drawing.Point(20, startY + 3 * heightStep);
            this.lignesGroupBox.Size = new System.Drawing.Size(550, 280);
            
            this.lignesDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lignesDataGridView.Height = 210;
            this.lignesDataGridView.AutoGenerateColumns = false;
            this.lignesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lignesDataGridView.MultiSelect = false;
            this.lignesDataGridView.AllowUserToAddRows = false;
            this.lignesDataGridView.AllowUserToDeleteRows = false;
            this.lignesDataGridView.ReadOnly = true;
            
            // Add ligne button
            this.addLigneButton.Location = new System.Drawing.Point(225, 220);
            this.addLigneButton.Size = new System.Drawing.Size(100, 30);
            this.addLigneButton.Text = "Ajouter ligne";
            this.addLigneButton.Click += new System.EventHandler(this.AddLigneButton_Click);
            
            // Add the DataGridView to the GroupBox
            this.lignesGroupBox.Controls.Add(this.lignesDataGridView);
            this.lignesGroupBox.Controls.Add(this.addLigneButton);
            
            // Buttons
            this.saveButton.Location = new System.Drawing.Point(200, 420);
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.Text = "Sauvegarder";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            
            this.cancelButton.Location = new System.Drawing.Point(310, 420);
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dateLabel, this.dateTimePicker,
                this.clientLabel, this.clientComboBox,
                this.lignesGroupBox,
                this.saveButton, this.cancelButton
            });
            
            this.lignesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lignesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
} 