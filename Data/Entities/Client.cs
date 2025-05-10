using System;
using System.Collections.Generic;

namespace StockApp.Data.Entities
{
    public class Client
    {
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string MatFiscal { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public decimal Credit { get; set; }

        public List<FactureVente> FacturesVente { get; set; } = new List<FactureVente>();
    }
} 