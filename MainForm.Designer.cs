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
            this.kryptonPalette = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.tabControl = new Krypton.Navigator.KryptonNavigator();
            this.clientsPage = new Krypton.Navigator.KryptonPage();
            this.fournisseursPage = new Krypton.Navigator.KryptonPage();
            this.piecesPage = new Krypton.Navigator.KryptonPage();
            this.facturesVentePage = new Krypton.Navigator.KryptonPage();
            this.facturesAchatPage = new Krypton.Navigator.KryptonPage();
            this.utilisateursPage = new Krypton.Navigator.KryptonPage();
            this.mouvementsStockPage = new Krypton.Navigator.KryptonPage();
            this.searchTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.searchButton = new Krypton.Toolkit.KryptonButton();
            this.searchPanel = new Krypton.Toolkit.KryptonPanel();
            this.headerPanel = new Krypton.Toolkit.KryptonPanel();
            this.headerLabel = new Krypton.Toolkit.KryptonLabel();
            
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fournisseursPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piecesPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturesVentePage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturesAchatPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utilisateursPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouvementsStockPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).BeginInit();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            
            // headerPanel
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.headerPanel.Size = new System.Drawing.Size(1000, 60);
            this.headerPanel.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.headerPanel.StateCommon.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Controls.Add(this.headerLabel);
            
            // headerLabel
            this.headerLabel.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            this.headerLabel.Location = new System.Drawing.Point(20, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(250, 30);
            this.headerLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.headerLabel.StateCommon.ShortText.Font = new System.Drawing.Font("SF Pro Display", 18F, System.Drawing.FontStyle.Bold);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Values.Text = "Gestion de Stock";
            
            // Search Panel
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Height = 70;
            this.searchPanel.Location = new System.Drawing.Point(0, 60);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.searchPanel.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.searchPanel.Size = new System.Drawing.Size(1000, 70);
            this.searchPanel.TabIndex = 1;
            this.searchPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            
            // Search Button
            this.searchButton.Location = new System.Drawing.Point(320, 15);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(120, 40);
            this.searchButton.TabIndex = 1;
            this.searchButton.Values.Text = "Rechercher";
            this.searchButton.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.searchButton.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.searchButton.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.searchButton.StateCommon.Border.Rounding = 10;
            this.searchButton.StateCommon.Border.Width = 0;
            this.searchButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.searchButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F, System.Drawing.FontStyle.Bold);
            
            // Search TextBox
            this.searchTextBox.Location = new System.Drawing.Point(20, 15);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(280, 40);
            this.searchTextBox.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.searchTextBox.StateCommon.Border.Rounding = 10;
            this.searchTextBox.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.searchTextBox.StateCommon.Content.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.CueHint.CueHintText = "Recherche...";
            
            // TabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 130);
            this.tabControl.Name = "tabControl";
            this.tabControl.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
                this.clientsPage,
                this.fournisseursPage,
                this.piecesPage,
                this.facturesVentePage,
                this.facturesAchatPage,
                this.utilisateursPage,
                this.mouvementsStockPage});
            this.tabControl.Size = new System.Drawing.Size(1000, 470);
            this.tabControl.TabIndex = 2;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.StateCommon.Tab.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tabControl.StateCommon.Tab.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tabControl.StateCommon.Tab.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) | Krypton.Toolkit.PaletteDrawBorders.Left) | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.tabControl.StateCommon.Tab.Border.Width = 0;
            this.tabControl.StateCommon.Tab.Content.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F);
            this.tabControl.StateCommon.Tab.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.tabControl.StateSelected.Tab.Back.Color1 = System.Drawing.Color.White;
            this.tabControl.StateSelected.Tab.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.tabControl.StateSelected.Tab.Content.ShortText.Font = new System.Drawing.Font("SF Pro Display", 10F, System.Drawing.FontStyle.Bold);
            
            // TabPages
            this.clientsPage.Text = "Clients";
            this.clientsPage.ToolTipTitle = "Gestion des Clients";
            this.clientsPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.clientsPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.clientsPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.fournisseursPage.Text = "Fournisseurs";
            this.fournisseursPage.ToolTipTitle = "Gestion des Fournisseurs";
            this.fournisseursPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.fournisseursPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.fournisseursPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.piecesPage.Text = "Pièces";
            this.piecesPage.ToolTipTitle = "Gestion des Pièces";
            this.piecesPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.piecesPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.piecesPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.facturesVentePage.Text = "Factures Vente";
            this.facturesVentePage.ToolTipTitle = "Gestion des Factures de Vente";
            this.facturesVentePage.MinimumSize = new System.Drawing.Size(50, 50);
            this.facturesVentePage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.facturesVentePage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.facturesAchatPage.Text = "Factures Achat";
            this.facturesAchatPage.ToolTipTitle = "Gestion des Factures d'Achat";
            this.facturesAchatPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.facturesAchatPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.facturesAchatPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.utilisateursPage.Text = "Utilisateurs";
            this.utilisateursPage.ToolTipTitle = "Gestion des Utilisateurs";
            this.utilisateursPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.utilisateursPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.utilisateursPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            this.mouvementsStockPage.Text = "Mouvements Stock";
            this.mouvementsStockPage.ToolTipTitle = "Gestion des Mouvements de Stock";
            this.mouvementsStockPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.mouvementsStockPage.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.mouvementsStockPage.StateCommon.Page.Color1 = System.Drawing.Color.White;
            
            // Form
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "MainForm";
            this.Text = "Gestion de Stock";
            
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientsPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fournisseursPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piecesPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturesVentePage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturesAchatPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utilisateursPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouvementsStockPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPanel)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonPalette;
        private Krypton.Navigator.KryptonNavigator tabControl;
        private Krypton.Navigator.KryptonPage clientsPage;
        private Krypton.Navigator.KryptonPage fournisseursPage;
        private Krypton.Navigator.KryptonPage piecesPage;
        private Krypton.Navigator.KryptonPage facturesVentePage;
        private Krypton.Navigator.KryptonPage facturesAchatPage;
        private Krypton.Navigator.KryptonPage utilisateursPage;
        private Krypton.Navigator.KryptonPage mouvementsStockPage;
        private Krypton.Toolkit.KryptonTextBox searchTextBox;
        private Krypton.Toolkit.KryptonButton searchButton;
        private Krypton.Toolkit.KryptonPanel searchPanel;
        private Krypton.Toolkit.KryptonPanel headerPanel;
        private Krypton.Toolkit.KryptonLabel headerLabel;
    }
} 