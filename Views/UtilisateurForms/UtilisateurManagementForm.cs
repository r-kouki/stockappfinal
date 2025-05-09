using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.UtilisateurForms
{
    public partial class UtilisateurManagementForm : UserControl
    {
        // Liste des utilisateurs en mémoire pour démonstration
        private List<User> users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "poly",
                PasswordHash = "root123",
                Nom = "Admin",
                Prenom = "System",
                Role = Role.ADMIN,
                Actif = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                PasswordHash = "user",
                Nom = "Utilisateur",
                Prenom = "Normal",
                Role = Role.OPERATEUR,
                Actif = true
            }
        };
        
        public UtilisateurManagementForm()
        {
            InitializeComponent();
            
            // Vérifier si l'utilisateur actuel est un administrateur
            if (LoginForm.CurrentUser.Role != Role.ADMIN)
            {
                // Afficher un message et désactiver les contrôles
                this.Controls.Clear();
                Label accessDeniedLabel = new Label
                {
                    Text = "Accès réservé aux administrateurs",
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold),
                    AutoSize = true,
                    Location = new System.Drawing.Point(50, 50)
                };
                this.Controls.Add(accessDeniedLabel);
                return;
            }
            
            // Charger les données des utilisateurs
            RefreshUsersGrid();
        }
        
        private void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new UtilisateurDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                // Ajouter le nouvel utilisateur à la collection
                users.Add(detailsForm.User);
                
                // Rafraîchir la grille
                RefreshUsersGrid();
                
                // Enregistrer l'action dans le log
                LogActivity($"Ajout de l'utilisateur {detailsForm.User.Username}");
                
                MessageBox.Show("Utilisateur ajouté avec succès!", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (this.usersDataGridView.SelectedRows.Count > 0)
            {
                // Récupérer l'utilisateur sélectionné
                var selectedUser = this.usersDataGridView.SelectedRows[0].DataBoundItem as User;
                
                if (selectedUser != null)
                {
                    // Créer une copie de l'utilisateur pour l'édition
                    var userCopy = new User
                    {
                        Id = selectedUser.Id,
                        Username = selectedUser.Username,
                        PasswordHash = selectedUser.PasswordHash,
                        Nom = selectedUser.Nom,
                        Prenom = selectedUser.Prenom,
                        Role = selectedUser.Role,
                        Actif = selectedUser.Actif
                    };
                    
                    var detailsForm = new UtilisateurDetailsForm(userCopy);
                    
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Trouver l'utilisateur original et le mettre à jour
                        var originalUser = users.Find(u => u.Id == detailsForm.User.Id);
                        if (originalUser != null)
                        {
                            originalUser.Username = detailsForm.User.Username;
                            originalUser.PasswordHash = detailsForm.User.PasswordHash;
                            originalUser.Nom = detailsForm.User.Nom;
                            originalUser.Prenom = detailsForm.User.Prenom;
                            originalUser.Role = detailsForm.User.Role;
                            originalUser.Actif = detailsForm.User.Actif;
                            
                            // Rafraîchir la grille
                            RefreshUsersGrid();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Modification de l'utilisateur {originalUser.Username}");
                            
                            MessageBox.Show("Utilisateur modifié avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.usersDataGridView.SelectedRows.Count > 0)
            {
                var selectedUser = this.usersDataGridView.SelectedRows[0].DataBoundItem as User;
                
                if (selectedUser != null)
                {
                    // Empêcher la suppression de son propre compte
                    if (selectedUser.Id == LoginForm.CurrentUser.Id)
                    {
                        MessageBox.Show("Vous ne pouvez pas supprimer votre propre compte.", 
                            "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer l'utilisateur {selectedUser.Username}?", 
                        "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        // Supprimer de notre collection
                        string username = selectedUser.Username;
                        users.Remove(selectedUser);
                        
                        // Rafraîchir la grille
                        RefreshUsersGrid();
                        
                        // Enregistrer l'action dans le log
                        LogActivity($"Suppression de l'utilisateur {username}");
                        
                        MessageBox.Show("Utilisateur supprimé avec succès!", "Succès", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void RefreshUsersGrid()
        {
            // Mettre à jour la source de données pour rafraîchir la grille
            this.usersDataGridView.DataSource = null;
            this.usersDataGridView.DataSource = users;
        }
        
        private void LogActivity(string activity)
        {
            try
            {
                // Dans une application réelle, vous enregistreriez dans une base de données ou un fichier
                string logEntry = $"{DateTime.Now} - {LoginForm.CurrentUser.Username} - {activity}";
                System.Diagnostics.Debug.WriteLine(logEntry);
                
                // Ajouter au fichier de log
                string logPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "activity_log.txt");
                
                System.IO.File.AppendAllText(logPath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur d'enregistrement dans le log: {ex.Message}");
            }
        }
    }
} 