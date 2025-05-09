using System;
using System.Linq;

namespace StockApp.Data.Entities
{
    public class FactureVente : Facture
    {
        public Guid ClientId { get; set; } // Foreign Key
        public Client? Client { get; set; } // Navigation Property

        public override decimal TotalHT()
        {
            // Calculation logic: sum of (LigneFacture.Quantite * LigneFacture.PrixUnitaireHT * (1 - LigneFacture.RemisePct/100))
            return LignesFacture.Sum(ligne => ligne.Quantite * ligne.PrixUnitaireHT * (1 - ligne.RemisePct / 100));
        }

        public override decimal TotalTTC()
        {
            // Calculation logic: sum of (LigneFacture.Quantite * LigneFacture.PrixUnitaireHT * (1 - LigneFacture.RemisePct/100) * (1 + Piece.TvaPct/100))
            // This requires LigneFacture to have a reference to Piece to get TvaPct
            // For simplicity here, let's assume a global TVA or it's calculated differently.
            // A more accurate calculation would involve joining with Piece or storing TvaPct per line.
            // return LignesFacture.Sum(ligne => ligne.Quantite * ligne.PrixUnitaireHT * (1 - ligne.RemisePct / 100) * (1 + (ligne.Piece?.TvaPct ?? 0) / 100));
            // Simplified: just adds a fixed TVA for example purposes, or relies on TotalHT and an overall TVA if applicable.
            // To implement it correctly as per the diagram, LigneFacture should allow access to Piece.TvaPct
            //decimal totalHt = TotalHT(); // This line is not used
            // Assuming an average or most common TVA if not calculated per line with specific piece TVA
            // This part needs careful implementation based on business rules for TVA calculation on sales invoices.
            // For now, returning HT as TTC if TVA logic is complex and not fully defined for aggregate.
            // Or, let's assume each LigneFacture.Piece.TvaPct is used.
             return LignesFacture.Sum(ligne => 
                (ligne.Quantite * ligne.PrixUnitaireHT * (1 - ligne.RemisePct / 100)) * 
                (1 + (ligne.Piece?.TvaPct ?? 0) / 100)
            );
        }
    }
} 