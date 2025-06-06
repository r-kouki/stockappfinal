namespace StockApp.PieceForms
{
    partial class PieceManagementForm
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
            this.piecesDataGridView = new System.Windows.Forms.DataGridView();
            this.marqueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prixAchatHTColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prixVenteHTColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seuilAlerteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvaPctColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.piecesDataGridView)).BeginInit();
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
            this.addButton.Text = "Ajouter Pièce";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            this.editButton.Location = new System.Drawing.Point(140, 10);
            this.editButton.Size = new System.Drawing.Size(120, 25);
            this.editButton.Text = "Modifier Pièce";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(270, 10);
            this.deleteButton.Size = new System.Drawing.Size(120, 25);
            this.deleteButton.Text = "Supprimer Pièce";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            // DataGridView Setup
            this.piecesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.piecesDataGridView.AutoGenerateColumns = false;
            this.piecesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.piecesDataGridView.MultiSelect = false;
            this.piecesDataGridView.AllowUserToAddRows = false;
            this.piecesDataGridView.AllowUserToDeleteRows = false;
            this.piecesDataGridView.ReadOnly = true;
            
            // DataGridView Columns
            this.marqueColumn.DataPropertyName = "Marque";
            this.marqueColumn.HeaderText = "Marque";
            this.marqueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.referenceColumn.DataPropertyName = "Reference";
            this.referenceColumn.HeaderText = "Référence";
            this.referenceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.prixAchatHTColumn.DataPropertyName = "PrixAchatHT";
            this.prixAchatHTColumn.HeaderText = "Prix Achat HT";
            this.prixAchatHTColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prixAchatHTColumn.DefaultCellStyle.Format = "C2";
            
            this.prixVenteHTColumn.DataPropertyName = "PrixVenteHT";
            this.prixVenteHTColumn.HeaderText = "Prix Vente HT";
            this.prixVenteHTColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prixVenteHTColumn.DefaultCellStyle.Format = "C2";
            
            this.stockColumn.DataPropertyName = "Stock";
            this.stockColumn.HeaderText = "Stock";
            this.stockColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.seuilAlerteColumn.DataPropertyName = "SeuilAlerte";
            this.seuilAlerteColumn.HeaderText = "Seuil Alerte";
            this.seuilAlerteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.tvaPctColumn.DataPropertyName = "TvaPct";
            this.tvaPctColumn.HeaderText = "TVA %";
            this.tvaPctColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tvaPctColumn.DefaultCellStyle.Format = "P2";

            // Add columns to DataGridView
            this.piecesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.marqueColumn,
                this.referenceColumn,
                this.prixAchatHTColumn,
                this.prixVenteHTColumn,
                this.stockColumn,
                this.seuilAlerteColumn,
                this.tvaPctColumn
            });

            // UserControl Setup
            this.Controls.Add(this.piecesDataGridView);
            this.Controls.Add(this.buttonPanel);
            this.Size = new System.Drawing.Size(800, 450);
            
            ((System.ComponentModel.ISupportInitialize)(this.piecesDataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView piecesDataGridView;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn marqueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prixAchatHTColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prixVenteHTColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn seuilAlerteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvaPctColumn;
    }
} 