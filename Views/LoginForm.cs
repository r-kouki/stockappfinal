using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using Krypton.Toolkit;

namespace StockApp
{
    public partial class LoginForm : KryptonForm
    {
        // Static property to store the current logged in user
        public static User CurrentUser { get; private set; }
        
        private readonly IUserRepository _userRepository;
        
        public LoginForm(IUserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
            
            // Apply iOS-inspired look with built-in palette
            this.PaletteMode = PaletteMode.SparklePurple;
            
            // Override button styles directly
            ApplyIOSButtonStyle(loginButton);
            ApplyIOSCancelButtonStyle(cancelButton);
            
            // Style TextBoxes
            ApplyIOSTextBoxStyle(usernameTextBox);
            ApplyIOSTextBoxStyle(passwordTextBox);
            
            // Style Labels
            ApplyIOSLabelStyle(titleLabel, true);
            ApplyIOSLabelStyle(usernameLabel, false);
            ApplyIOSLabelStyle(passwordLabel, false);
            
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Margin = new Padding(10);
            this.BackColor = Color.FromArgb(247, 247, 247); // iOS light gray background
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
            button.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
        
        private void ApplyIOSCancelButtonStyle(KryptonButton button)
        {
            button.StateCommon.Back.Color1 = Color.FromArgb(247, 247, 247);
            button.StateCommon.Back.Color2 = Color.FromArgb(247, 247, 247);
            button.StateCommon.Border.Color1 = Color.FromArgb(200, 200, 200);
            button.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            button.StateCommon.Border.Rounding = 10;
            button.StateCommon.Border.Width = 1;
            button.StateCommon.Content.ShortText.Color1 = Color.FromArgb(90, 90, 90);
            button.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 10F);
        }
        
        private void ApplyIOSTextBoxStyle(KryptonTextBox textBox)
        {
            textBox.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            textBox.StateCommon.Border.Rounding = 10;
            textBox.StateCommon.Border.Color1 = Color.FromArgb(200, 200, 200);
            textBox.StateCommon.Content.Font = new Font("Segoe UI", 10F);
        }
        
        private void ApplyIOSLabelStyle(KryptonLabel label, bool isTitle)
        {
            if (isTitle)
            {
                label.StateCommon.ShortText.Color1 = Color.FromArgb(0, 122, 255);
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            }
            else
            {
                label.StateCommon.ShortText.Color1 = Color.FromArgb(90, 90, 90);
                label.StateCommon.ShortText.Font = new Font("Segoe UI", 10F);
            }
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