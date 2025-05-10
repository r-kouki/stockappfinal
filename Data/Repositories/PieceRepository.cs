using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class PieceRepository : Repository<Piece>, IPieceRepository
    {
        public PieceRepository(StockContext context, IIdGeneratorService idGenerator) 
            : base(context, idGenerator)
        {
        }

        public async Task<Piece> GetWithStockMovementsAsync(string id)
        {
            return await _context.Pieces
                .Include(p => p.MouvementsStock)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateStockAsync(string pieceId, int quantityChange)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Use direct database query with locking to prevent concurrency issues
                    var piece = await _context.Pieces
                        .FromSqlRaw("SELECT * FROM Pieces WHERE Id = {0} FOR UPDATE", pieceId)
                        .FirstOrDefaultAsync();

                    if (piece != null)
                    {
                        // Update the stock quantity
                        piece.Stock += quantityChange;
                        
                        // Save changes within the transaction
                        await _context.SaveChangesAsync();
                        
                        // Commit the transaction
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        throw new Exception($"Pièce avec ID {pieceId} non trouvée");
                    }
                }
                catch (Exception ex)
                {
                    // Roll back the transaction on error
                    await transaction.RollbackAsync();
                    throw new Exception($"Erreur lors de la mise à jour du stock: {ex.Message}", ex);
                }
            }
        }
    }
} 