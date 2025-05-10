using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApp.Data.Entities
{
    public class FactureAchat : Facture
    {
        public string FournisseurId { get; set; } = string.Empty;
        public Fournisseur? Fournisseur { get; set; }
        
        public string? NumeroFactureFournisseur { get; set; }
        
        [Column("DateEcheance")]
        public DateTime? DateEcheance { get; set; }
        
        public string? Note { get; set; }
        public decimal MontantPaye { get; set; }
        
        // Propriétés calculées
        public decimal RestantAPayer => TotalTTC() - MontantPaye;
        public bool EstPayee => MontantPaye >= TotalTTC();
        
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
    }
} 