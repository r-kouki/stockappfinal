using System;
using System.Windows.Forms;
using StockApp.Data.Entities;

namespace StockApp.UtilisateurForms
{
    public partial class UtilisateurDetailsForm : Form
    {
        private User _user;
        private bool _isNewUser;

        public UtilisateurDetailsForm(User user = null)
        {
            InitializeComponent();
            
            _user = user ?? new User { Id = string.Empty, Actif = true };
            _isNewUser = user == null;
            
            // Configuration du titre du formulaire
            this.Text = _isNewUser ? "Ajouter un utilisateur" : "Modifier un utilisateur";
            
            // Remplir le combobox des rôles
            roleComboBox.DataSource = Enum.GetValues(typeof(Role));
            
            // Charger les données de l'utilisateur si en mode édition
            if (!_isNewUser)
            {
                this.usernameTextBox.Text = _user.Username;
                this.passwordTextBox.Text = _user.PasswordHash;
                this.nomTextBox.Text = _user.Nom;
                this.prenomTextBox.Text = _user.Prenom;
                this.roleComboBox.SelectedItem = _user.Role;
                this.actifCheckBox.Checked = _user.Actif;
                
                // Si nous modifions l'utilisateur actuel, désactiver la possibilité de changer le rôle
                if (_user.Id == LoginForm.CurrentUser.Id)
                {
                    this.roleComboBox.Enabled = false;
                    this.actifCheckBox.Enabled = false;
                }
            }
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Valider les entrées
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("Le nom d'utilisateur est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Le mot de passe est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return;
            }
            
            // Mettre à jour l'objet utilisateur avec les valeurs du formulaire
            _user.Username = usernameTextBox.Text.Trim();
            _user.PasswordHash = passwordTextBox.Text.Trim(); // Dans une vraie app, ça serait hashé
            _user.Nom = nomTextBox.Text.Trim();
            _user.Prenom = prenomTextBox.Text.Trim();
            _user.Role = (Role)roleComboBox.SelectedItem;
            _user.Actif = actifCheckBox.Checked;
            
            // Définir le DialogResult pour indiquer le succès
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        // Propriété pour accéder aux données de l'utilisateur
        public User User => _user;
    }
} 