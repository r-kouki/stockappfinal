using System;

namespace StockApp.Data.Entities
{
    public class LigneFacture
    {
        public Guid Id { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaireHT { get; set; }
        public decimal RemisePct { get; set; }

        public Guid PieceId { get; set; } // Foreign Key
        public Piece? Piece { get; set; } // Navigation Property

        public Guid FactureId { get; set; } // Foreign Key
        public Facture? Facture { get; set; } // Navigation Property
    }
} 