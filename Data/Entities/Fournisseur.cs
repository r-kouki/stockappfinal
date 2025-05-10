using System;
using System.Collections.Generic;

namespace StockApp.Data.Entities
{
    public class Fournisseur
    {
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty; // Assuming prenom can exist for a contact person
        public string MatFiscal { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public decimal Credit { get; set; } // Or Dette if it's what the company owes them

        public List<FactureAchat> FacturesAchat { get; set; } = new List<FactureAchat>();
    }
} 