using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class MouvementStockRepository : Repository<MouvementStock>, IMouvementStockRepository
    {
        public MouvementStockRepository(StockContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MouvementStock>> GetAllWithDetailsAsync()
        {
            return await _context.MouvementsStock
                .Include(m => m.Piece)
                .ToListAsync();
        }

        public async Task<IEnumerable<MouvementStock>> GetByPieceAsync(Guid pieceId)
        {
            return await _context.MouvementsStock
                .Where(m => m.PieceId == pieceId)
                .Include(m => m.Piece)
                .ToListAsync();
        }
    }
} 