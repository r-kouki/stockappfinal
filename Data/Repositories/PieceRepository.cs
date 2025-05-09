using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class PieceRepository : Repository<Piece>, IPieceRepository
    {
        public PieceRepository(StockContext context) : base(context)
        {
        }

        public async Task<Piece> GetWithStockMovementsAsync(Guid id)
        {
            return await _context.Pieces
                .Include(p => p.MouvementsStock)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateStockAsync(Guid id, int quantityChange)
        {
            var piece = await GetByIdAsync(id);
            if (piece != null)
            {
                piece.Stock += quantityChange;
                await UpdateAsync(piece);
            }
        }
    }
} 