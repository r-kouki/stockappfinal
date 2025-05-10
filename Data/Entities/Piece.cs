using System;
using System.Collections.Generic;

namespace StockApp.Data.Entities
{
    public class Piece
    {
        public string Id { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public decimal PrixAchatHT { get; set; }
        public decimal PrixVenteHT { get; set; }
        public int Stock { get; set; }
        public int SeuilAlerte { get; set; }
        public decimal TvaPct { get; set; }

        public List<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();
        public List<MouvementStock> MouvementsStock { get; set; } = new List<MouvementStock>();
    }
} 