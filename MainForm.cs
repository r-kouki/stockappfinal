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

namespace StockApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
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
        
        private void ApplyUserRolePermissions()
        {
            // If not admin, disable delete functionality and user management
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                // Hide the User Management tab for non-admin users
                this.tabControl.TabPages.Remove(this.utilisateursPage);
                
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