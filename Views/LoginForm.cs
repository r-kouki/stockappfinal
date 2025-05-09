using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;

namespace StockApp
{
    public partial class LoginForm : Form
    {
        // Static property to store the current logged in user
        public static User CurrentUser { get; private set; }
        
        private readonly IUserRepository _userRepository;
        
        public LoginForm(IUserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
        }
        
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.", 
                    "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Disable login button during authentication
            loginButton.Enabled = false;
            
            try
            {
                // Find user with matching credentials using the repository
                User user = await _userRepository.LoginAsync(username, password);
                
                if (user != null)
                {
                    // Set the current user and open the main form
                    CurrentUser = user;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", 
                        "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'authentification: {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable login button
                loginButton.Enabled = true;
            }
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
} 