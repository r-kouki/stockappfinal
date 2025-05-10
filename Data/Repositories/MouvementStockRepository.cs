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
        public MouvementStockRepository(StockContext context, IIdGeneratorService idGenerator) 
            : base(context, idGenerator)
        {
        }

        public async Task<IEnumerable<MouvementStock>> GetAllWithDetailsAsync()
        {
            return await _context.MouvementsStock
                .Include(m => m.Piece)
                .ToListAsync();
        }

        public async Task<IEnumerable<MouvementStock>> GetByPieceIdAsync(string pieceId)
        {
            return await _context.MouvementsStock
                .Where(m => m.PieceId == pieceId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<MouvementStock>> GetByFactureIdAsync(string factureId)
        {
            return await _context.MouvementsStock
                .Where(m => m.FactureId == factureId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
} 