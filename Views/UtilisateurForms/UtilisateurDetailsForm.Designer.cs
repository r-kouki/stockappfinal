namespace StockApp.UtilisateurForms
{
    partial class UtilisateurDetailsForm
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.nomLabel = new System.Windows.Forms.Label();
            this.nomTextBox = new System.Windows.Forms.TextBox();
            this.prenomLabel = new System.Windows.Forms.Label();
            this.prenomTextBox = new System.Windows.Forms.TextBox();
            this.roleLabel = new System.Windows.Forms.Label();
            this.roleComboBox = new System.Windows.Forms.ComboBox();
            this.actifCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            
            this.SuspendLayout();
            
            // Form setup
            this.Size = new System.Drawing.Size(400, 350);
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
            
            // Username
            this.usernameLabel.Location = new System.Drawing.Point(labelX, startY);
            this.usernameLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.usernameLabel.Text = "Nom d'utilisateur:";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.usernameTextBox.Location = new System.Drawing.Point(controlX, startY);
            this.usernameTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Password
            this.passwordLabel.Location = new System.Drawing.Point(labelX, startY + heightStep);
            this.passwordLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.passwordLabel.Text = "Mot de passe:";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.passwordTextBox.Location = new System.Drawing.Point(controlX, startY + heightStep);
            this.passwordTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Nom
            this.nomLabel.Location = new System.Drawing.Point(labelX, startY + 2 * heightStep);
            this.nomLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.nomLabel.Text = "Nom:";
            this.nomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.nomTextBox.Location = new System.Drawing.Point(controlX, startY + 2 * heightStep);
            this.nomTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Prenom
            this.prenomLabel.Location = new System.Drawing.Point(labelX, startY + 3 * heightStep);
            this.prenomLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.prenomLabel.Text = "Prénom:";
            this.prenomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.prenomTextBox.Location = new System.Drawing.Point(controlX, startY + 3 * heightStep);
            this.prenomTextBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            
            // Role
            this.roleLabel.Location = new System.Drawing.Point(labelX, startY + 4 * heightStep);
            this.roleLabel.Size = new System.Drawing.Size(labelWidth, controlHeight);
            this.roleLabel.Text = "Rôle:";
            this.roleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            
            this.roleComboBox.Location = new System.Drawing.Point(controlX, startY + 4 * heightStep);
            this.roleComboBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.roleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            // Actif
            this.actifCheckBox.Location = new System.Drawing.Point(controlX, startY + 5 * heightStep);
            this.actifCheckBox.Size = new System.Drawing.Size(controlWidth, controlHeight);
            this.actifCheckBox.Text = "Utilisateur actif";
            this.actifCheckBox.Checked = true;
            
            // Buttons
            this.saveButton.Location = new System.Drawing.Point(controlX - 50, startY + 7 * heightStep);
            this.saveButton.Size = new System.Drawing.Size(100, 30);
            this.saveButton.Text = "Sauvegarder";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            
            this.cancelButton.Location = new System.Drawing.Point(controlX + 60, startY + 7 * heightStep);
            this.cancelButton.Size = new System.Drawing.Size(100, 30);
            this.cancelButton.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.usernameLabel, this.usernameTextBox,
                this.passwordLabel, this.passwordTextBox,
                this.nomLabel, this.nomTextBox,
                this.prenomLabel, this.prenomTextBox,
                this.roleLabel, this.roleComboBox,
                this.actifCheckBox,
                this.saveButton, this.cancelButton
            });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label nomLabel;
        private System.Windows.Forms.TextBox nomTextBox;
        private System.Windows.Forms.Label prenomLabel;
        private System.Windows.Forms.TextBox prenomTextBox;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.ComboBox roleComboBox;
        private System.Windows.Forms.CheckBox actifCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
} 