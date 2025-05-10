using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IPieceRepository : IRepository<Piece>
    {
        Task<Piece> GetWithStockMovementsAsync(string id);
        Task UpdateStockAsync(string pieceId, int quantityChange);
    }
} 