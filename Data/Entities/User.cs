using System;

namespace StockApp.Data.Entities
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Salt { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool Actif { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
} 