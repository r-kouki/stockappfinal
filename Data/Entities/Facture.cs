using System;
using System.Collections.Generic;

namespace StockApp.Data.Entities
{
    public abstract class Facture
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();

        // Methods to be implemented by derived classes or as calculated properties
        public abstract decimal TotalHT();
        public abstract decimal TotalTTC();
    }
} 