using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class FactureAchatRepository : Repository<FactureAchat>, IFactureAchatRepository
    {
        private readonly IMouvementStockRepository _mouvementStockRepository;
        private readonly IPieceRepository _pieceRepository;

        public FactureAchatRepository(StockContext context, 
            IMouvementStockRepository mouvementStockRepository,
            IPieceRepository pieceRepository) : base(context)
        {
            _mouvementStockRepository = mouvementStockRepository;
            _pieceRepository = pieceRepository;
        }

        public async Task<FactureAchat> GetWithDetailsAsync(Guid id)
        {
            return await _context.FacturesAchat
                .Include(f => f.Fournisseur)
                .Include(f => f.LignesFacture)
                    .ThenInclude(lf => lf.Piece)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<FactureAchat>> GetAllWithDetailsAsync()
        {
            return await _context.FacturesAchat
                .Include(f => f.Fournisseur)
                .Include(f => f.LignesFacture)
                    .ThenInclude(lf => lf.Piece)
                .ToListAsync();
        }

        public override async Task AddAsync(FactureAchat facture)
        {
            await base.AddAsync(facture);

            // Create stock movements for each line
            if (facture.LignesFacture != null)
            {
                foreach (var ligne in facture.LignesFacture)
                {
                    // Create stock movement (ENTREE)
                    var mouvement = new MouvementStock
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Type = "ENTREE",
                        Quantite = ligne.Quantite,
                        PieceId = ligne.PieceId,
                        FactureId = facture.Id
                    };

                    await _mouvementStockRepository.AddAsync(mouvement);

                    // Update stock quantity
                    await _pieceRepository.UpdateStockAsync(ligne.PieceId, ligne.Quantite);
                }
            }
        }

        public override async Task DeleteAsync(Guid id)
        {
            var facture = await GetWithDetailsAsync(id);
            if (facture != null)
            {
                // Reverse stock movements
                foreach (var ligne in facture.LignesFacture)
                {
                    // Update stock quantity (reverse)
                    await _pieceRepository.UpdateStockAsync(ligne.PieceId, -ligne.Quantite);
                }

                // Delete related movements
                var mouvements = await _context.MouvementsStock
                    .Where(m => m.FactureId == id)
                    .ToListAsync();

                _context.MouvementsStock.RemoveRange(mouvements);

                // Then delete the invoice
                await base.DeleteAsync(id);
            }
        }
    }
} 