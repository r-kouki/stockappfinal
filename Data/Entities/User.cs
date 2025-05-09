using System;

namespace StockApp.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool Actif { get; set; }
    }
} 