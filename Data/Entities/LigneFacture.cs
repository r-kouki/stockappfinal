using System;

namespace StockApp.Data.Entities
{
    public class LigneFacture
    {
        public string Id { get; set; } = string.Empty;
        public int Quantite { get; set; }
        public decimal PrixUnitaireHT { get; set; }
        public decimal RemisePct { get; set; }

        public string FactureId { get; set; } = string.Empty;
        public Facture? Facture { get; set; }

        public string PieceId { get; set; } = string.Empty;
        public Piece? Piece { get; set; }
        
        // Calculated properties
        public decimal TotalLigneHT() => Quantite * PrixUnitaireHT * (1 - RemisePct / 100);
        public decimal TotalLigneTTC() => TotalLigneHT() * (1 + (Piece?.TvaPct ?? 0) / 100);
    }
} 