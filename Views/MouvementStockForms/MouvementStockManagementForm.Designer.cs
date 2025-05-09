namespace StockApp.MouvementStockForms
{
    partial class MouvementStockManagementForm
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
            this.mouvementsDataGridView = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantiteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pieceIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factureIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.mouvementsDataGridView)).BeginInit();
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
            this.addButton.Size = new System.Drawing.Size(140, 25);
            this.addButton.Text = "Ajouter Mouvement";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            this.editButton.Location = new System.Drawing.Point(160, 10);
            this.editButton.Size = new System.Drawing.Size(140, 25);
            this.editButton.Text = "Modifier Mouvement";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(310, 10);
            this.deleteButton.Size = new System.Drawing.Size(140, 25);
            this.deleteButton.Text = "Supprimer Mouvement";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            // DataGridView Setup
            this.mouvementsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mouvementsDataGridView.AutoGenerateColumns = false;
            this.mouvementsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mouvementsDataGridView.MultiSelect = false;
            this.mouvementsDataGridView.AllowUserToAddRows = false;
            this.mouvementsDataGridView.AllowUserToDeleteRows = false;
            this.mouvementsDataGridView.ReadOnly = true;
            
            // DataGridView Columns
            this.idColumn.DataPropertyName = "Id";
            this.idColumn.HeaderText = "ID";
            this.idColumn.Width = 60;
            
            this.dateColumn.DataPropertyName = "Date";
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.typeColumn.DataPropertyName = "Type";
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.quantiteColumn.DataPropertyName = "Quantite";
            this.quantiteColumn.HeaderText = "Quantité";
            this.quantiteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.pieceIdColumn.DataPropertyName = "PieceId";
            this.pieceIdColumn.HeaderText = "Pièce ID";
            this.pieceIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.factureIdColumn.DataPropertyName = "FactureId";
            this.factureIdColumn.HeaderText = "Facture ID";
            this.factureIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            // Add columns to DataGridView
            this.mouvementsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.idColumn,
                this.dateColumn,
                this.typeColumn,
                this.quantiteColumn,
                this.pieceIdColumn,
                this.factureIdColumn
            });

            // UserControl Setup
            this.Controls.Add(this.mouvementsDataGridView);
            this.Controls.Add(this.buttonPanel);
            this.Size = new System.Drawing.Size(800, 450);
            
            ((System.ComponentModel.ISupportInitialize)(this.mouvementsDataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView mouvementsDataGridView;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantiteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pieceIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn factureIdColumn;
    }
}