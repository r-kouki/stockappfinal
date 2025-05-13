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
            components = new System.ComponentModel.Container();
            kryptonPalette = new Krypton.Toolkit.KryptonCustomPaletteBase(components);
            tabControl = new Krypton.Navigator.KryptonNavigator();
            clientsPage = new Krypton.Navigator.KryptonPage();
            fournisseursPage = new Krypton.Navigator.KryptonPage();
            piecesPage = new Krypton.Navigator.KryptonPage();
            facturesVentePage = new Krypton.Navigator.KryptonPage();
            facturesAchatPage = new Krypton.Navigator.KryptonPage();
            utilisateursPage = new Krypton.Navigator.KryptonPage();
            mouvementsStockPage = new Krypton.Navigator.KryptonPage();
            searchPanel = new Krypton.Toolkit.KryptonPanel();
            headerPanel = new Krypton.Toolkit.KryptonPanel();
            headerLabel = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)tabControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clientsPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fournisseursPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)piecesPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)facturesVentePage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)facturesAchatPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)utilisateursPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mouvementsStockPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)searchPanel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)headerPanel).BeginInit();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonPalette
            // 
            kryptonPalette.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // tabControl
            // 
            tabControl.ControlKryptonFormFeatures = false;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 130);
            tabControl.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            tabControl.Owner = null;
            tabControl.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            tabControl.Pages.AddRange(new Krypton.Navigator.KryptonPage[] { clientsPage, fournisseursPage, piecesPage, facturesVentePage, facturesAchatPage, utilisateursPage, mouvementsStockPage });
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1004, 458);
            tabControl.StateCommon.Tab.Back.Color1 = Color.FromArgb(247, 247, 247);
            tabControl.StateCommon.Tab.Back.Color2 = Color.FromArgb(247, 247, 247);
            tabControl.StateCommon.Tab.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            tabControl.StateCommon.Tab.Border.Width = 0;
            tabControl.StateCommon.Tab.Content.ShortText.Color1 = Color.FromArgb(90, 90, 90);
            tabControl.StateCommon.Tab.Content.ShortText.Font = new Font("Microsoft Sans Serif", 10F);
            tabControl.StateSelected.Tab.Back.Color1 = Color.White;
            tabControl.StateSelected.Tab.Content.ShortText.Color1 = Color.FromArgb(0, 122, 255);
            tabControl.StateSelected.Tab.Content.ShortText.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            tabControl.TabIndex = 2;
            // 
            // clientsPage
            // 
            clientsPage.AutoHiddenSlideSize = new Size(200, 200);
            clientsPage.Flags = 65534;
            clientsPage.LastVisibleSet = true;
            clientsPage.MinimumSize = new Size(50, 50);
            clientsPage.Name = "clientsPage";
            clientsPage.Size = new Size(1002, 433);
            clientsPage.StateCommon.Page.Color1 = Color.White;
            clientsPage.Text = "Clients";
            clientsPage.ToolTipTitle = "Gestion des Clients";
            clientsPage.UniqueName = "f1bcdbf20aee460ba2ea49e4223882e6";
            // 
            // fournisseursPage
            // 
            fournisseursPage.AutoHiddenSlideSize = new Size(200, 200);
            fournisseursPage.Flags = 65534;
            fournisseursPage.LastVisibleSet = true;
            fournisseursPage.MinimumSize = new Size(50, 50);
            fournisseursPage.Name = "fournisseursPage";
            fournisseursPage.Size = new Size(150, 100);
            fournisseursPage.StateCommon.Page.Color1 = Color.White;
            fournisseursPage.Text = "Fournisseurs";
            fournisseursPage.ToolTipTitle = "Gestion des Fournisseurs";
            fournisseursPage.UniqueName = "b0aa19873661424f9b1424154c7cfd51";
            // 
            // piecesPage
            // 
            piecesPage.AutoHiddenSlideSize = new Size(200, 200);
            piecesPage.Flags = 65534;
            piecesPage.LastVisibleSet = true;
            piecesPage.MinimumSize = new Size(50, 50);
            piecesPage.Name = "piecesPage";
            piecesPage.Size = new Size(150, 100);
            piecesPage.StateCommon.Page.Color1 = Color.White;
            piecesPage.Text = "Pièces";
            piecesPage.ToolTipTitle = "Gestion des Pièces";
            piecesPage.UniqueName = "4aec28ca760946f8809b239af5cc5074";
            // 
            // facturesVentePage
            // 
            facturesVentePage.AutoHiddenSlideSize = new Size(200, 200);
            facturesVentePage.Flags = 65534;
            facturesVentePage.LastVisibleSet = true;
            facturesVentePage.MinimumSize = new Size(50, 50);
            facturesVentePage.Name = "facturesVentePage";
            facturesVentePage.Size = new Size(150, 100);
            facturesVentePage.StateCommon.Page.Color1 = Color.White;
            facturesVentePage.Text = "Factures Vente";
            facturesVentePage.ToolTipTitle = "Gestion des Factures de Vente";
            facturesVentePage.UniqueName = "0ec0eb910e4546f09875a129872a371f";
            // 
            // facturesAchatPage
            // 
            facturesAchatPage.AutoHiddenSlideSize = new Size(200, 200);
            facturesAchatPage.Flags = 65534;
            facturesAchatPage.LastVisibleSet = true;
            facturesAchatPage.MinimumSize = new Size(50, 50);
            facturesAchatPage.Name = "facturesAchatPage";
            facturesAchatPage.Size = new Size(150, 100);
            facturesAchatPage.StateCommon.Page.Color1 = Color.White;
            facturesAchatPage.Text = "Factures Achat";
            facturesAchatPage.ToolTipTitle = "Gestion des Factures d'Achat";
            facturesAchatPage.UniqueName = "cf9427377dc54c5fb3b81ea09df61d51";
            // 
            // utilisateursPage
            // 
            utilisateursPage.AutoHiddenSlideSize = new Size(200, 200);
            utilisateursPage.Flags = 65534;
            utilisateursPage.LastVisibleSet = true;
            utilisateursPage.MinimumSize = new Size(50, 50);
            utilisateursPage.Name = "utilisateursPage";
            utilisateursPage.Size = new Size(150, 100);
            utilisateursPage.StateCommon.Page.Color1 = Color.White;
            utilisateursPage.Text = "Utilisateurs";
            utilisateursPage.ToolTipTitle = "Gestion des Utilisateurs";
            utilisateursPage.UniqueName = "b67101b151d542ef98880da6228e4dbd";
            // 
            // mouvementsStockPage
            // 
            mouvementsStockPage.AutoHiddenSlideSize = new Size(200, 200);
            mouvementsStockPage.Flags = 65534;
            mouvementsStockPage.LastVisibleSet = true;
            mouvementsStockPage.MinimumSize = new Size(50, 50);
            mouvementsStockPage.Name = "mouvementsStockPage";
            mouvementsStockPage.Size = new Size(150, 100);
            mouvementsStockPage.StateCommon.Page.Color1 = Color.White;
            mouvementsStockPage.Text = "Mouvements Stock";
            mouvementsStockPage.ToolTipTitle = "Gestion des Mouvements de Stock";
            mouvementsStockPage.UniqueName = "80deeb6062c744c49dfca5cae923fea8";
            // 
            // searchPanel
            // 
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 60);
            searchPanel.Name = "searchPanel";
            searchPanel.Padding = new Padding(20, 15, 20, 15);
            searchPanel.Size = new Size(1004, 70);
            searchPanel.StateCommon.Color1 = Color.FromArgb(247, 247, 247);
            searchPanel.TabIndex = 1;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(headerLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            headerPanel.Size = new Size(1004, 60);
            headerPanel.StateCommon.Color1 = Color.FromArgb(247, 247, 247);
            headerPanel.StateCommon.Color2 = Color.FromArgb(247, 247, 247);
            headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            headerLabel.LabelStyle = Krypton.Toolkit.LabelStyle.TitlePanel;
            headerLabel.Location = new Point(20, 15);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(209, 33);
            headerLabel.StateCommon.ShortText.Color1 = Color.FromArgb(0, 122, 255);
            headerLabel.StateCommon.ShortText.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            headerLabel.TabIndex = 0;
            headerLabel.Values.Text = "Gestion de Stock";
            // 
            // MainForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1004, 588);
            Controls.Add(tabControl);
            Controls.Add(searchPanel);
            Controls.Add(headerPanel);
            Name = "MainForm";
            Text = "Gestion de Stock";
            ((System.ComponentModel.ISupportInitialize)tabControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)clientsPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)fournisseursPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)piecesPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)facturesVentePage).EndInit();
            ((System.ComponentModel.ISupportInitialize)facturesAchatPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)utilisateursPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)mouvementsStockPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)searchPanel).EndInit();
            ((System.ComponentModel.ISupportInitialize)headerPanel).EndInit();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
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
        private Krypton.Toolkit.KryptonPanel searchPanel;
        private Krypton.Toolkit.KryptonPanel headerPanel;
        private Krypton.Toolkit.KryptonLabel headerLabel;
    }
} 