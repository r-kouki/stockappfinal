namespace StockApp.UtilisateurForms
{
    partial class UtilisateurManagementForm
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
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.usernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prenomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actifColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
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
            this.addButton.Text = "Ajouter Utilisateur";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);

            this.editButton.Location = new System.Drawing.Point(140, 10);
            this.editButton.Size = new System.Drawing.Size(120, 25);
            this.editButton.Text = "Modifier Utilisateur";
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(270, 10);
            this.deleteButton.Size = new System.Drawing.Size(120, 25);
            this.deleteButton.Text = "Supprimer Utilisateur";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            // DataGridView Setup
            this.usersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersDataGridView.AutoGenerateColumns = false;
            this.usersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGridView.MultiSelect = false;
            this.usersDataGridView.AllowUserToAddRows = false;
            this.usersDataGridView.AllowUserToDeleteRows = false;
            this.usersDataGridView.ReadOnly = true;
            
            // DataGridView Columns
            this.usernameColumn.DataPropertyName = "Username";
            this.usernameColumn.HeaderText = "Nom d'utilisateur";
            this.usernameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.nomColumn.DataPropertyName = "Nom";
            this.nomColumn.HeaderText = "Nom";
            this.nomColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.prenomColumn.DataPropertyName = "Prenom";
            this.prenomColumn.HeaderText = "Prénom";
            this.prenomColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.roleColumn.DataPropertyName = "Role";
            this.roleColumn.HeaderText = "Rôle";
            this.roleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            
            this.actifColumn.DataPropertyName = "Actif";
            this.actifColumn.HeaderText = "Actif";
            this.actifColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            // Add columns to DataGridView
            this.usersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.usernameColumn,
                this.nomColumn,
                this.prenomColumn,
                this.roleColumn,
                this.actifColumn
            });

            // UserControl Setup
            this.Controls.Add(this.usersDataGridView);
            this.Controls.Add(this.buttonPanel);
            this.Size = new System.Drawing.Size(800, 450);
            
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prenomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn actifColumn;
    }
} 