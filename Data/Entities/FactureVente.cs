using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApp.Data.Entities
{
    public class FactureVente : Facture
    {
        public string ClientId { get; set; } = string.Empty;
        public Client? Client { get; set; }
        
        public string? NumeroFactureClient { get; set; }
        
        [Column("DateEcheance")]
        public DateTime? DateEcheance { get; set; }
        
        public string? Note { get; set; }
        public decimal MontantPaye { get; set; }
        
        // Calculated properties
        public decimal RestantAPayer => TotalTTC() - MontantPaye;
        public bool EstPayee => MontantPaye >= TotalTTC();
        public string StatutPaiement => EstPayee ? "Payée" : "Non payée";
        
        // Methods for calculating invoice totals
        public override decimal TotalHT()
        {
            if (LignesFacture == null || !LignesFacture.Any())
                return 0;
                
            return LignesFacture.Sum(l => l.TotalLigneHT());
        }
        
        public override decimal TotalTTC()
        {
            if (LignesFacture == null || !LignesFacture.Any())
                return 0;
                
            return LignesFacture.Sum(l => l.TotalLigneTTC());
        }
        
        // Other business logic methods can be added here
        public decimal CalculerTVA()
        {
            return TotalTTC() - TotalHT();
        }
    }
} 