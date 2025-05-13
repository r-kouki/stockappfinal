using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data.Entities;
using Krypton.Toolkit;
using Krypton.Navigator;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp
{
    public partial class MainForm : KryptonForm
    {
        public MainForm()
        {
            InitializeComponent();
            
            // Apply iOS-inspired look with built-in palette
            this.PaletteMode = PaletteMode.SparklePurple;
            
            // Override button styles directly
            foreach (Control control in this.Controls)
            {
                if (control is KryptonButton button)
                {
                    ApplyIOSButtonStyle(button);
                }
                else if (control.HasChildren)
                {
                    foreach (Control childControl in control.Controls)
                    {
                        if (childControl is KryptonButton childButton)
                        {
                            ApplyIOSButtonStyle(childButton);
                        }
                    }
                }
            }
            
            // Update form appearance
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Margin = new Padding(10);
            this.BackColor = Color.FromArgb(247, 247, 247); // iOS light gray background
            
            // Update the form title to include the current user
            this.Text = $"Gestion de Stock - {LoginForm.CurrentUser.Prenom} {LoginForm.CurrentUser.Nom} [{LoginForm.CurrentUser.Role}]";
            
            // Load all tab management UIs
            LoadClientManagement();
            LoadFournisseurManagement();
            LoadPieceManagement();
            LoadFactureVenteManagement();
            LoadFactureAchatManagement();
            LoadUtilisateurManagement();
            LoadMouvementStockManagement();
            
            // Apply permissions based on user role
            ApplyUserRolePermissions();
        }



        private void ApplyIOSButtonStyle(KryptonButton button)
        {
            button.StateCommon.Back.Color1 = Color.FromArgb(0, 122, 255);
            button.StateCommon.Back.Color2 = Color.FromArgb(0, 122, 255);
            button.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            button.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            button.StateCommon.Border.Rounding = 10;
            button.StateCommon.Border.Width = 0;
            button.StateCommon.Content.ShortText.Color1 = Color.White;
            button.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }
        
        private void ApplyUserRolePermissions()
        {
            // If not admin, disable delete functionality and user management
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                // Hide the User Management tab for non-admin users
                this.tabControl.Pages.Remove(this.utilisateursPage);
                
                // Disable delete buttons in client management
                foreach (Control control in clientsPage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
                
                // Disable delete buttons in fournisseur management
                foreach (Control control in fournisseursPage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
                
                // Disable delete buttons in piece management
                foreach (Control control in piecesPage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
                
                // Disable delete buttons in facture vente management
                foreach (Control control in facturesVentePage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
                
                // Disable delete buttons in facture achat management
                foreach (Control control in facturesAchatPage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
                
                // Disable delete buttons in mouvement stock management
                foreach (Control control in mouvementsStockPage.Controls)
                {
                    if (control is UserControl)
                    {
                        DisableDeleteButtons(control);
                    }
                }
            }
        }
        
        private void DisableDeleteButtons(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is Button button && (button.Name.Contains("delete") || button.Text.Contains("Supprimer")))
                {
                    button.Enabled = false;
                    button.Text += " (Admin)";
                }
                else if (control is KryptonButton kryptonButton && (kryptonButton.Name.Contains("delete") || kryptonButton.Values.Text.Contains("Supprimer")))
                {
                    kryptonButton.Enabled = false;
                    kryptonButton.Values.Text += " (Admin)";
                }
                else if (control.Controls.Count > 0)
                {
                    DisableDeleteButtons(control);
                }
            }
        }
        
        private void LoadClientManagement()
        {
            try
            {
                // Create and add the ClientManagementForm to the Clients tab
                var clientManagement = new StockApp.ClientForms.ClientManagementForm();
                clientManagement.Dock = DockStyle.Fill;
                this.clientsPage.Controls.Add(clientManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading client management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadFournisseurManagement()
        {
            try
            {
                // Create and add the FournisseurManagementForm to the Fournisseurs tab
                var fournisseurManagement = new StockApp.FournisseurForms.FournisseurManagementForm();
                fournisseurManagement.Dock = DockStyle.Fill;
                this.fournisseursPage.Controls.Add(fournisseurManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fournisseur management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadPieceManagement()
        {
            try
            {
                // Create and add the PieceManagementForm to the Pieces tab
                var pieceManagement = new StockApp.PieceForms.PieceManagementForm();
                pieceManagement.Dock = DockStyle.Fill;
                this.piecesPage.Controls.Add(pieceManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading piece management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadFactureVenteManagement()
        {
            try
            {
                // Create and add the FactureVenteManagementForm to the Factures Vente tab
                var factureVenteManagement = new StockApp.FactureForms.FactureVenteManagementForm();
                factureVenteManagement.Dock = DockStyle.Fill;
                this.facturesVentePage.Controls.Add(factureVenteManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading facture vente management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadFactureAchatManagement()
        {
            try
            {
                // Create and add the FactureAchatManagementForm to the Factures Achat tab
                var factureAchatManagement = new StockApp.FactureForms.FactureAchatManagementForm();
                factureAchatManagement.Dock = DockStyle.Fill;
                this.facturesAchatPage.Controls.Add(factureAchatManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading facture achat management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadUtilisateurManagement()
        {
            try
            {
                // Create and add the UtilisateurManagementForm to the Utilisateurs tab
                var utilisateurManagement = new StockApp.UtilisateurForms.UtilisateurManagementForm();
                utilisateurManagement.Dock = DockStyle.Fill;
                this.utilisateursPage.Controls.Add(utilisateurManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading utilisateur management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadMouvementStockManagement()
        {
            try
            {
                // Create and add the MouvementStockManagementForm to the Mouvements Stock tab
                var mouvementStockManagement = new StockApp.MouvementStockForms.MouvementStockManagementForm();
                mouvementStockManagement.Dock = DockStyle.Fill;
                this.mouvementsStockPage.Controls.Add(mouvementStockManagement);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading mouvement stock management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 