using System;
using System.Linq;

namespace StockApp.Data.Entities
{
    public class FactureAchat : Facture
    {
        public Guid FournisseurId { get; set; } // Foreign Key
        public Fournisseur? Fournisseur { get; set; } // Navigation Property

        public override decimal TotalHT()
        {
            // Calculation logic for purchase invoice lines
            return LignesFacture.Sum(ligne => ligne.Quantite * ligne.PrixUnitaireHT * (1 - ligne.RemisePct / 100));
        }

        public override decimal TotalTTC()
        {
            // Purchase invoices might handle TVA differently, sometimes it's deductible and not added to cost of goods
            // or it's based on the supplier's TVA rules.
            // For this example, we'll assume a similar calculation to FactureVente for demonstration.
            // The actual calculation depends heavily on accounting practices for purchases.
            return LignesFacture.Sum(ligne =>
                (ligne.Quantite * ligne.PrixUnitaireHT * (1 - ligne.RemisePct / 100)) *
                (1 + (ligne.Piece?.TvaPct ?? 0) / 100) // Assuming TvaPct on Piece is also applicable for purchases
            );
        }
    }
} 