using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IMouvementStockRepository : IRepository<MouvementStock>
    {
        Task<IEnumerable<MouvementStock>> GetAllWithDetailsAsync();
        Task<IEnumerable<MouvementStock>> GetByPieceIdAsync(string pieceId);
        Task<IEnumerable<MouvementStock>> GetByFactureIdAsync(string factureId);
    }
} 