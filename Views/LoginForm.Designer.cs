namespace StockApp
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            this.usernameLabel = new Krypton.Toolkit.KryptonLabel();
            this.usernameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.passwordLabel = new Krypton.Toolkit.KryptonLabel();
            this.passwordTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.loginButton = new Krypton.Toolkit.KryptonButton();
            this.cancelButton = new Krypton.Toolkit.KryptonButton();
            this.titleLabel = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPalette = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.headerPanel = new Krypton.Toolkit.KryptonPanel();
            
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            
            // headerPanel
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.headerPanel.Size = new System.Drawing.Size(400, 70);
            this.headerPanel.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.headerPanel.StateCommon.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.headerPanel.TabIndex = 7;
            this.headerPanel.Controls.Add(this.titleLabel);
            
            // titleLabel
            this.titleLabel.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.titleLabel.Location = new System.Drawing.Point(100, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(200, 30);
            this.titleLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.titleLabel.StateCommon.ShortText.Font = new System.Drawing.Font("SF Pro Display", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Values.Text = "Gestion de Stock";
            
            // usernameLabel
            this.usernameLabel.Location = new System.Drawing.Point(50, 90);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(120, 20);
            this.usernameLabel.StateCommon.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.usernameLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Values.Text = "Nom d'utilisateur:";
            
            // usernameTextBox
            this.usernameTextBox.Location = new System.Drawing.Point(50, 110);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(300, 35);
            this.usernameTextBox.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.usernameTextBox.StateCommon.Border.Rounding = 10;
            this.usernameTextBox.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.usernameTextBox.StateCommon.Content.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.usernameTextBox.TabIndex = 2;
            
            // passwordLabel
            this.passwordLabel.Location = new System.Drawing.Point(50, 155);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(120, 20);
            this.passwordLabel.StateCommon.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.passwordLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Values.Text = "Mot de passe:";
            
            // passwordTextBox
            this.passwordTextBox.Location = new System.Drawing.Point(50, 175);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(300, 35);
            this.passwordTextBox.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.passwordTextBox.StateCommon.Border.Rounding = 10;
            this.passwordTextBox.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.passwordTextBox.StateCommon.Content.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.passwordTextBox.TabIndex = 4;
            
            // loginButton
            this.loginButton.Location = new System.Drawing.Point(80, 235);
            this.loginButton.Name = "loginButton";
            this.loginButton.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.loginButton.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.loginButton.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.loginButton.Size = new System.Drawing.Size(110, 40);
            this.loginButton.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.loginButton.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.loginButton.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.loginButton.StateCommon.Border.Rounding = 10;
            this.loginButton.StateCommon.Border.Width = 0;
            this.loginButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.loginButton.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.loginButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F, System.Drawing.FontStyle.Bold);
            this.loginButton.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(240)))));
            this.loginButton.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(240)))));
            this.loginButton.StatePressed.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.loginButton.StatePressed.Border.Width = 0;
            this.loginButton.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(240)))));
            this.loginButton.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(240)))));
            this.loginButton.TabIndex = 5;
            this.loginButton.Values.Text = "Connexion";
            this.loginButton.Click += new System.EventHandler(this.LoginButton_Click);
            
            // cancelButton
            this.cancelButton.Location = new System.Drawing.Point(210, 235);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.cancelButton.Size = new System.Drawing.Size(110, 40);
            this.cancelButton.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.cancelButton.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.cancelButton.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cancelButton.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cancelButton.StateCommon.Border.Rounding = 10;
            this.cancelButton.StateCommon.Border.Width = 1;
            this.cancelButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cancelButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Values.Text = "Annuler";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            
            // LoginForm
            this.AcceptButton = this.loginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion";
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel titleLabel;
        private Krypton.Toolkit.KryptonLabel usernameLabel;
        private Krypton.Toolkit.KryptonTextBox usernameTextBox;
        private Krypton.Toolkit.KryptonLabel passwordLabel;
        private Krypton.Toolkit.KryptonTextBox passwordTextBox;
        private Krypton.Toolkit.KryptonButton loginButton;
        private Krypton.Toolkit.KryptonButton cancelButton;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonPalette;
        private Krypton.Toolkit.KryptonPanel headerPanel;
    }
} 