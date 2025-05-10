using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.Data.Repositories
{
    public class FactureAchatRepository : Repository<FactureAchat>, IFactureAchatRepository
    {
        private readonly IMouvementStockRepository _mouvementStockRepository;
        private readonly IPieceRepository _pieceRepository;
        private readonly IIdGeneratorService _idGenerator;

        public FactureAchatRepository(
            StockContext context, 
            IIdGeneratorService idGenerator,
            IMouvementStockRepository mouvementStockRepository,
            IPieceRepository pieceRepository) 
            : base(context, idGenerator)
        {
            _mouvementStockRepository = mouvementStockRepository;
            _pieceRepository = pieceRepository;
            _idGenerator = idGenerator;
        }

        public async Task<FactureAchat> GetWithDetailsAsync(string id)
        {
            return await _context.FacturesAchat
                .Include(f => f.Fournisseur)
                .Include(f => f.LignesFacture)
                    .ThenInclude(l => l.Piece)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<FactureAchat>> GetAllWithDetailsAsync()
        {
            return await _context.FacturesAchat
                .Include(f => f.Fournisseur)
                .Include(f => f.LignesFacture)
                    .ThenInclude(l => l.Piece)
                .OrderByDescending(f => f.Date)
                .ToListAsync();
        }

        public override async Task AddAsync(FactureAchat facture)
        {
            // Make sure we're using a new context for this operation to avoid tracking conflicts
            using (var scope = Program.ServiceProvider.CreateScope())
            {
                var newContext = scope.ServiceProvider.GetRequiredService<StockContext>();
                
                try
                {
                    // Start a transaction to ensure data consistency
                    using (var transaction = await newContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            // Save the invoice entity first without lines
                            var lignesFacture = facture.LignesFacture?.ToList();
                            facture.LignesFacture = null;
                            
                            // Set a custom ID if not set already
                            var idProperty = facture.GetType().GetProperty("Id");
                            if (idProperty != null && idProperty.PropertyType == typeof(string))
                            {
                                var currentId = idProperty.GetValue(facture) as string;
                                if (string.IsNullOrEmpty(currentId))
                                {
                                    // Generate new ID based on entity type
                                    var entityName = facture.GetType().Name;
                                    var newId = _idGenerator.GenerateId(entityName);
                                    idProperty.SetValue(facture, newId);
                                }
                            }
                            
                            // Add the invoice without lines
                            await newContext.FacturesAchat.AddAsync(facture);
                            await newContext.SaveChangesAsync();
                            
                            // Add each line with a reference to the invoice
                            if (lignesFacture != null)
                            {
                                foreach (var ligne in lignesFacture)
                                {
                                    // Set a custom ID if not set already
                                    var ligneIdProperty = ligne.GetType().GetProperty("Id");
                                    if (ligneIdProperty != null && ligneIdProperty.PropertyType == typeof(string))
                                    {
                                        var currentLigneId = ligneIdProperty.GetValue(ligne) as string;
                                        if (string.IsNullOrEmpty(currentLigneId))
                                        {
                                            // Generate new ID based on entity type
                                            var entityName = ligne.GetType().Name;
                                            var newId = _idGenerator.GenerateId(entityName);
                                            ligneIdProperty.SetValue(ligne, newId);
                                        }
                                    }
                                    
                                    // Set the invoice ID
                                    ligne.FactureId = facture.Id;
                                    
                                    // Add the line
                                    await newContext.LignesFacture.AddAsync(ligne);
                                    await newContext.SaveChangesAsync();
                                    
                                    // Create stock movement (ENTREE) directly
                                    var mouvement = new MouvementStock
                                    {
                                        Date = DateTime.Now,
                                        Type = "ENTREE",
                                        Quantite = ligne.Quantite,
                                        PieceId = ligne.PieceId,
                                        FactureId = facture.Id
                                    };
                                    
                                    // Add directly to context
                                    await newContext.MouvementsStock.AddAsync(mouvement);
                                    await newContext.SaveChangesAsync();
                                    
                                    // Update stock quantity directly
                                    var piece = await newContext.Pieces.FindAsync(ligne.PieceId);
                                    if (piece != null)
                                    {
                                        piece.Stock += ligne.Quantite; // Add to stock for purchase
                                        await newContext.SaveChangesAsync();
                                    }
                                }
                            }
                            
                            // Commit the transaction
                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if there's an error
                            await transaction.RollbackAsync();
                            throw new Exception($"Error adding invoice: {ex.Message}", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in AddAsync: {ex.Message}");
                    throw;
                }
            }
        }

        public override async Task DeleteAsync(string id)
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

        public override async Task UpdateAsync(FactureAchat facture)
        {
            // Make sure we're using a new context for this operation to avoid tracking conflicts
            using (var scope = Program.ServiceProvider.CreateScope())
            {
                var newContext = scope.ServiceProvider.GetRequiredService<StockContext>();
                
                try
                {
                    // Start a transaction to ensure data consistency
                    using (var transaction = await newContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            // Get the existing invoice with its lines
                            var existingFacture = await newContext.FacturesAchat
                                .Include(f => f.LignesFacture)
                                .FirstOrDefaultAsync(f => f.Id == facture.Id);
                            
                            if (existingFacture == null)
                            {
                                throw new Exception($"Invoice with ID {facture.Id} not found");
                            }
                            
                            // Update invoice properties
                            existingFacture.Date = facture.Date;
                            existingFacture.DateEcheance = facture.DateEcheance;
                            existingFacture.FournisseurId = facture.FournisseurId;
                            existingFacture.NumeroFactureFournisseur = facture.NumeroFactureFournisseur;
                            existingFacture.Note = facture.Note;
                            existingFacture.MontantPaye = facture.MontantPaye;
                            
                            // Save changes to the invoice
                            await newContext.SaveChangesAsync();
                            
                            // Handle invoice lines - we'll keep track of lines to add/update/delete
                            var newLines = facture.LignesFacture?.ToList() ?? new List<LigneFacture>();
                            var existingLines = existingFacture.LignesFacture?.ToList() ?? new List<LigneFacture>();
                            
                            // Lines to add (in new but not in existing)
                            var linesToAdd = newLines
                                .Where(nl => !existingLines.Any(el => el.Id == nl.Id || (string.IsNullOrEmpty(nl.Id) && el.PieceId == nl.PieceId)))
                                .ToList();
                            
                            // Lines to update (in both)
                            var linesToUpdate = newLines
                                .Where(nl => !string.IsNullOrEmpty(nl.Id) && existingLines.Any(el => el.Id == nl.Id))
                                .ToList();
                            
                            // Lines to delete (in existing but not in new)
                            var linesToDelete = existingLines
                                .Where(el => !newLines.Any(nl => nl.Id == el.Id || (string.IsNullOrEmpty(nl.Id) && el.PieceId == nl.PieceId)))
                                .ToList();
                            
                            // Delete lines
                            foreach (var lineToDelete in linesToDelete)
                            {
                                // Reduce stock directly
                                var piece = await newContext.Pieces.FindAsync(lineToDelete.PieceId);
                                if (piece != null)
                                {
                                    piece.Stock -= lineToDelete.Quantite; // Remove from stock since we're removing a purchase
                                    await newContext.SaveChangesAsync();
                                }
                                
                                // Delete related stock movements
                                var mouvements = await newContext.MouvementsStock
                                    .Where(m => m.FactureId == facture.Id && m.PieceId == lineToDelete.PieceId)
                                    .ToListAsync();
                                    
                                newContext.MouvementsStock.RemoveRange(mouvements);
                                
                                // Delete the line
                                newContext.LignesFacture.Remove(lineToDelete);
                                await newContext.SaveChangesAsync();
                            }
                            
                            // Update lines
                            foreach (var lineToUpdate in linesToUpdate)
                            {
                                var existingLine = existingLines.First(el => el.Id == lineToUpdate.Id);
                                int quantityDifference = lineToUpdate.Quantite - existingLine.Quantite;
                                
                                if (quantityDifference != 0)
                                {
                                    // Update stock directly
                                    var piece = await newContext.Pieces.FindAsync(lineToUpdate.PieceId);
                                    if (piece != null)
                                    {
                                        piece.Stock += quantityDifference; // Positive for purchase
                                        await newContext.SaveChangesAsync();
                                    }
                                    
                                    // Create stock movement if needed directly
                                    if (quantityDifference > 0)
                                    {
                                        // Adding more stock (ENTREE)
                                        var mouvement = new MouvementStock
                                        {
                                            Date = DateTime.Now,
                                            Type = "ENTREE",
                                            Quantite = quantityDifference,
                                            PieceId = lineToUpdate.PieceId,
                                            FactureId = facture.Id
                                        };
                                        
                                        await newContext.MouvementsStock.AddAsync(mouvement);
                                        await newContext.SaveChangesAsync();
                                    }
                                    else if (quantityDifference < 0)
                                    {
                                        // Reducing stock (SORTIE)
                                        var mouvement = new MouvementStock
                                        {
                                            Date = DateTime.Now,
                                            Type = "SORTIE",
                                            Quantite = -quantityDifference, // Make it positive
                                            PieceId = lineToUpdate.PieceId,
                                            FactureId = facture.Id
                                        };
                                        
                                        await newContext.MouvementsStock.AddAsync(mouvement);
                                        await newContext.SaveChangesAsync();
                                    }
                                }
                                
                                // Update line properties
                                existingLine.Quantite = lineToUpdate.Quantite;
                                existingLine.PrixUnitaireHT = lineToUpdate.PrixUnitaireHT;
                                existingLine.RemisePct = lineToUpdate.RemisePct;
                                
                                // Save changes
                                await newContext.SaveChangesAsync();
                            }
                            
                            // Add new lines
                            foreach (var lineToAdd in linesToAdd)
                            {
                                // Set a custom ID if not set already
                                var ligneIdProperty = lineToAdd.GetType().GetProperty("Id");
                                if (ligneIdProperty != null && ligneIdProperty.PropertyType == typeof(string))
                                {
                                    var currentLigneId = ligneIdProperty.GetValue(lineToAdd) as string;
                                    if (string.IsNullOrEmpty(currentLigneId))
                                    {
                                        // Generate new ID based on entity type
                                        var entityName = lineToAdd.GetType().Name;
                                        var newId = _idGenerator.GenerateId(entityName);
                                        ligneIdProperty.SetValue(lineToAdd, newId);
                                    }
                                }
                                
                                // Set the invoice ID
                                lineToAdd.FactureId = facture.Id;
                                
                                // Add the line
                                await newContext.LignesFacture.AddAsync(lineToAdd);
                                await newContext.SaveChangesAsync();
                                
                                // Create stock movement (ENTREE) directly
                                var mouvement = new MouvementStock
                                {
                                    Date = DateTime.Now,
                                    Type = "ENTREE",
                                    Quantite = lineToAdd.Quantite,
                                    PieceId = lineToAdd.PieceId,
                                    FactureId = facture.Id
                                };
                                
                                await newContext.MouvementsStock.AddAsync(mouvement);
                                await newContext.SaveChangesAsync();
                                
                                // Update stock quantity directly
                                var piece = await newContext.Pieces.FindAsync(lineToAdd.PieceId);
                                if (piece != null)
                                {
                                    piece.Stock += lineToAdd.Quantite; // Add to stock for purchase
                                    await newContext.SaveChangesAsync();
                                }
                            }
                            
                            // Commit the transaction
                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if there's an error
                            await transaction.RollbackAsync();
                            throw new Exception($"Error updating invoice: {ex.Message}", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateAsync: {ex.Message}");
                    throw;
                }
            }
        }
    }
} 