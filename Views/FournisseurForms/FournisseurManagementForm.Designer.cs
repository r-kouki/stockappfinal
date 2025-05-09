namespace StockApp.FournisseurForms
{
    partial class FournisseurManagementForm
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
            this.fournisseursDataGridView = new System.Windows.Forms.DataGridView();
            this.nomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prenomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matFiscalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telephoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.fournisseursDataGridView)).BeginInit();
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
            this.addButton.Text = "Ajouter Fournisseur";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            this.editButton.Location = new System.Drawing.Point(140, 10);
            this.editButton.Size = new System.Drawing.Size(120, 25);
            this.editButton.Text = "Modifier Fournisseur";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(270, 10);
            this.deleteButton.Size = new System.Drawing.Size(120, 25);
            this.deleteButton.Text = "Supprimer Fournisseur";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            // DataGridView Setup
            this.fournisseursDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fournisseursDataGridView.AutoGenerateColumns = false;
            this.fournisseursDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fournisseursDataGridView.MultiSelect = false;
            this.fournisseursDataGridView.AllowUserToAddRows = false;
            this.fournisseursDataGridView.AllowUserToDeleteRows = false;
            this.fournisseursDataGridView.ReadOnly = true;
            
            // DataGridView Columns
            this.nomColumn.DataPropertyName = "Nom";
            this.nomColumn.HeaderText = "Nom";
            this.nomColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.prenomColumn.DataPropertyName = "Prenom";
            this.prenomColumn.HeaderText = "Prénom";
            this.prenomColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.matFiscalColumn.DataPropertyName = "MatFiscal";
            this.matFiscalColumn.HeaderText = "Matricule Fiscal";
            this.matFiscalColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.adresseColumn.DataPropertyName = "Adresse";
            this.adresseColumn.HeaderText = "Adresse";
            this.adresseColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.adresseColumn.Width = 200;
            
            this.telephoneColumn.DataPropertyName = "Telephone";
            this.telephoneColumn.HeaderText = "Téléphone";
            this.telephoneColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.creditColumn.DataPropertyName = "Credit";
            this.creditColumn.HeaderText = "Crédit";
            this.creditColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.creditColumn.DefaultCellStyle.Format = "C2";

            // Add columns to DataGridView
            this.fournisseursDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.nomColumn,
                this.prenomColumn,
                this.matFiscalColumn,
                this.adresseColumn,
                this.telephoneColumn,
                this.creditColumn
            });

            // UserControl Setup
            this.Controls.Add(this.fournisseursDataGridView);
            this.Controls.Add(this.buttonPanel);
            this.Size = new System.Drawing.Size(800, 450);
            
            ((System.ComponentModel.ISupportInitialize)(this.fournisseursDataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView fournisseursDataGridView;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prenomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn matFiscalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telephoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditColumn;
    }
} 