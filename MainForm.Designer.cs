namespace StockApp
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.clientsPage = new System.Windows.Forms.TabPage();
            this.fournisseursPage = new System.Windows.Forms.TabPage();
            this.piecesPage = new System.Windows.Forms.TabPage();
            this.facturesVentePage = new System.Windows.Forms.TabPage();
            this.facturesAchatPage = new System.Windows.Forms.TabPage();
            this.utilisateursPage = new System.Windows.Forms.TabPage();
            this.mouvementsStockPage = new System.Windows.Forms.TabPage();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            
            this.SuspendLayout();
            
            // Search Panel
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Height = 40;
            this.searchPanel.Padding = new System.Windows.Forms.Padding(10);
            
            // Search Button
            this.searchButton.Location = new System.Drawing.Point(320, 10);
            this.searchButton.Size = new System.Drawing.Size(100, 25);
            this.searchButton.Text = "Rechercher";
            
            // Search TextBox
            this.searchTextBox.Location = new System.Drawing.Point(10, 10);
            this.searchTextBox.Size = new System.Drawing.Size(300, 25);
            
            // TabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Controls.Add(this.clientsPage);
            this.tabControl.Controls.Add(this.fournisseursPage);
            this.tabControl.Controls.Add(this.piecesPage);
            this.tabControl.Controls.Add(this.facturesVentePage);
            this.tabControl.Controls.Add(this.facturesAchatPage);
            this.tabControl.Controls.Add(this.utilisateursPage);
            this.tabControl.Controls.Add(this.mouvementsStockPage);
            
            // TabPages
            this.clientsPage.Text = "Clients";
            this.fournisseursPage.Text = "Fournisseurs";
            this.piecesPage.Text = "Pièces";
            this.facturesVentePage.Text = "Factures Vente";
            this.facturesAchatPage.Text = "Factures Achat";
            this.utilisateursPage.Text = "Utilisateurs";
            this.mouvementsStockPage.Text = "Mouvements Stock";
            
            // Form
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.searchPanel);
            this.Text = "Gestion de Stock";
            this.Size = new System.Drawing.Size(1000, 600);
            
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage clientsPage;
        private System.Windows.Forms.TabPage fournisseursPage;
        private System.Windows.Forms.TabPage piecesPage;
        private System.Windows.Forms.TabPage facturesVentePage;
        private System.Windows.Forms.TabPage facturesAchatPage;
        private System.Windows.Forms.TabPage utilisateursPage;
        private System.Windows.Forms.TabPage mouvementsStockPage;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel searchPanel;
    }
} 