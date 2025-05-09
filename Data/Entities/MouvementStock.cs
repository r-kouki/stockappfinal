using System;

namespace StockApp.Data.Entities
{
    public class MouvementStock
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // e.g., "ENTREE", "SORTIE"
        public int Quantite { get; set; }

        public Guid PieceId { get; set; } // Foreign Key
        public Piece? Piece { get; set; } // Navigation Property

        public Guid? FactureId { get; set; } // Foreign Key (nullable for movements not tied to an invoice)
        public Facture? Facture { get; set; } // Navigation Property

        // The note "type peut être ENTREE ou SORTIE" from diagram can be a comment here or enforced by an enum if preferred.
        // public MouvementStockType TypeMouvement { get; set; } // Alternative using an enum
    }

    // public enum MouvementStockType { ENTREE, SORTIE, AJUSTEMENT_INV, RETOUR_CLIENT, RETOUR_FOURNISSEUR }
} 