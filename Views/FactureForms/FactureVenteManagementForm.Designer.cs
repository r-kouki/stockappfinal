namespace StockApp.FactureForms
{
    partial class FactureVenteManagementForm
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
            this.facturesDataGridView = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalHTColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalTTCColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.facturesDataGridView)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();

            // Button Panel Setup
            this.buttonPanel.Controls.Add(this.deleteButton);
            this.buttonPanel.Controls.Add(this.editButton);
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPanel.Height = 40;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);

            // Button Setup
            this.addButton.Location = new System.Drawing.Point(10, 10);
            this.addButton.Size = new System.Drawing.Size(120, 25);
            this.addButton.Text = "Ajouter Facture";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            this.editButton.Location = new System.Drawing.Point(140, 10);
            this.editButton.Size = new System.Drawing.Size(120, 25);
            this.editButton.Text = "Modifier Facture";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(270, 10);
            this.deleteButton.Size = new System.Drawing.Size(120, 25);
            this.deleteButton.Text = "Supprimer Facture";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            // DataGridView Setup
            this.facturesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.facturesDataGridView.AutoGenerateColumns = false;
            this.facturesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.facturesDataGridView.MultiSelect = false;
            this.facturesDataGridView.AllowUserToAddRows = false;
            this.facturesDataGridView.AllowUserToDeleteRows = false;
            this.facturesDataGridView.ReadOnly = true;
            
            // DataGridView Columns
            this.idColumn.DataPropertyName = "Id";
            this.idColumn.HeaderText = "ID";
            this.idColumn.Width = 60;
            
            this.dateColumn.DataPropertyName = "Date";
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.clientIdColumn.DataPropertyName = "ClientId";
            this.clientIdColumn.HeaderText = "Client ID";
            this.clientIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.totalHTColumn.HeaderText = "Total HT";
            this.totalHTColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.totalTTCColumn.HeaderText = "Total TTC";
            this.totalTTCColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            // Add columns to DataGridView
            this.facturesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.idColumn,
                this.dateColumn,
                this.clientIdColumn,
                this.totalHTColumn,
                this.totalTTCColumn
            });

            // UserControl Setup
            this.Controls.Add(this.facturesDataGridView);
            this.Controls.Add(this.buttonPanel);
            this.Size = new System.Drawing.Size(800, 450);
            
            ((System.ComponentModel.ISupportInitialize)(this.facturesDataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView facturesDataGridView;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalHTColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalTTCColumn;
    }
} 