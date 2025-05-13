using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockApp.Data.Entities;
using System.Linq;
using StockApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Data;
using StockApp.Data;
using System.Threading.Tasks;

namespace StockApp.UtilisateurForms
{
    public partial class UtilisateurManagementForm : UserControl
    {
        private readonly IUserRepository _userRepository;
        private readonly StockDataAdapter _dataAdapter;
        private BindingSource _usersBindingSource;
        
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
            
            // Récupérer le repository depuis l'injection de dépendances
            _userRepository = Program.ServiceProvider.GetRequiredService<IUserRepository>();
            
            // Initialiser le DataAdapter pour la liaison de données
            _dataAdapter = new StockDataAdapter();
            
            // Configuration du BindingSource
            _usersBindingSource = new BindingSource();
            _usersBindingSource.DataSource = _dataAdapter.GetDataSet();
            _usersBindingSource.DataMember = "Users";
            
            // Lier la grille à la source de données
            usersDataGridView.DataSource = _usersBindingSource;
            
            // Configurer les colonnes
            ConfigureColumns();
            
            // Charger les données
            LoadDataAsync();
        }
        
        private void ConfigureColumns()
        {
            usersDataGridView.AutoGenerateColumns = false;
            
            // Effacer les colonnes existantes
            usersDataGridView.Columns.Clear();
            
            // Ajouter les colonnes correspondant au schéma de données
            var idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            };
            
            var usernameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Nom d'utilisateur",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var nomColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var prenomColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Prenom",
                HeaderText = "Prénom",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var roleColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Rôle",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            
            var actifColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Actif",
                HeaderText = "Actif",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            
            // Ajouter les colonnes à la grille
            usersDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                idColumn,
                usernameColumn,
                nomColumn,
                prenomColumn,
                roleColumn,
                actifColumn
            });
        }
        
        private async void LoadDataAsync()
        {
            try
            {
                // Remplir la table des utilisateurs dans le DataSet
                _dataAdapter.FillUsersTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des utilisateurs: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void AddButton_Click(object sender, EventArgs e)
        {
            var detailsForm = new UtilisateurDetailsForm();
            
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Ajouter le nouvel utilisateur à la base de données
                    await _userRepository.AddAsync(detailsForm.User);
                    
                    // Rafraîchir le DataSet
                    _dataAdapter.FillUsersTable();
                    
                    // Enregistrer l'action dans le log
                    LogActivity($"Ajout de l'utilisateur {detailsForm.User.Username}");
                    
                    MessageBox.Show("Utilisateur ajouté avec succès!", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout de l'utilisateur: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (this.usersDataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    // Récupérer l'ID de l'utilisateur sélectionné depuis DataRowView
                    var selectedRow = usersDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                    if (selectedRow != null)
                    {
                        var userId = selectedRow["Id"].ToString();
                        
                        // Récupérer l'entité utilisateur depuis le repository
                        var user = await _userRepository.GetByIdAsync(userId);
                        
                        if (user != null)
                        {
                            var detailsForm = new UtilisateurDetailsForm(user);
                            
                            if (detailsForm.ShowDialog() == DialogResult.OK)
                            {
                                // Mettre à jour l'utilisateur dans la base de données
                                await _userRepository.UpdateAsync(detailsForm.User);
                                
                                // Rafraîchir le DataSet
                                _dataAdapter.FillUsersTable();
                                
                                // Enregistrer l'action dans le log
                                LogActivity($"Modification de l'utilisateur {detailsForm.User.Username}");
                                
                                MessageBox.Show("Utilisateur modifié avec succès!", "Succès", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la modification de l'utilisateur: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.usersDataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    // Récupérer l'ID de l'utilisateur sélectionné depuis DataRowView
                    var selectedRow = usersDataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                    if (selectedRow != null)
                    {
                        var userId = selectedRow["Id"].ToString();
                        var username = selectedRow["Username"].ToString();
                        
                        // Empêcher la suppression de son propre compte
                        if (userId == LoginForm.CurrentUser.Id)
                        {
                            MessageBox.Show("Vous ne pouvez pas supprimer votre propre compte.", 
                                "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer l'utilisateur {username}?", 
                            "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (result == DialogResult.Yes)
                        {
                            // Supprimer de la base de données
                            await _userRepository.DeleteAsync(userId);
                            
                            // Rafraîchir le DataSet
                            _dataAdapter.FillUsersTable();
                            
                            // Enregistrer l'action dans le log
                            LogActivity($"Suppression de l'utilisateur {username}");
                            
                            MessageBox.Show("Utilisateur supprimé avec succès!", "Succès", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression de l'utilisateur: {ex.Message}", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.", "Aucune sélection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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