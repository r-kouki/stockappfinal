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
        private new readonly IIdGeneratorService _idGenerator;

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
            try
            {
                // Validation de base
                if (string.IsNullOrEmpty(facture.FournisseurId))
                {
                    throw new Exception("La facture doit avoir un FournisseurId valide");
                }
                
                if (facture.LignesFacture == null || facture.LignesFacture.Count == 0)
                {
                    throw new Exception("La facture doit avoir au moins une ligne");
                }
                
                // Utiliser directement le contexte principal pour éviter les problèmes de multiples contextes
                _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
                
                // Vérifier si l'entité est déjà suivie et la détacher si nécessaire
                var existingEntry = _context.ChangeTracker.Entries<FactureAchat>()
                    .FirstOrDefault(e => e.Entity.Id == facture.Id);
                
                if (existingEntry != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Détachement d'une entité existante avec ID {facture.Id}");
                    existingEntry.State = EntityState.Detached;
                }
                
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Générer et vérifier l'ID de facture
                        if (string.IsNullOrEmpty(facture.Id))
                        {
                            facture.Id = await GenerateUniqueFactureIdAsync();
                            System.Diagnostics.Debug.WriteLine($"ID généré pour la facture: {facture.Id}");
                        }
                        else
                        {
                            // Vérifier si l'ID existe déjà dans la base de données
                            bool idExists = await _context.FacturesAchat.AnyAsync(f => f.Id == facture.Id);
                            if (idExists)
                            {
                                // Générer un nouvel ID si conflit
                                facture.Id = await GenerateUniqueFactureIdAsync();
                                System.Diagnostics.Debug.WriteLine($"ID en conflit, nouvel ID généré: {facture.Id}");
                            }
                        }
                        
                        // Vérifier à nouveau après génération d'ID si nécessaire
                        existingEntry = _context.ChangeTracker.Entries<FactureAchat>()
                            .FirstOrDefault(e => e.Entity.Id == facture.Id);
                        
                        if (existingEntry != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"Détachement d'une entité existante avec ID généré {facture.Id}");
                            existingEntry.State = EntityState.Detached;
                        }
                        
                        // 2. Stocker temporairement les lignes et les dissocier de la facture
                        var lignesFacture = facture.LignesFacture.ToList();
                        facture.LignesFacture = null;
                        
                        // 3. Enregistrer la facture seule d'abord
                        System.Diagnostics.Debug.WriteLine($"Ajout facture simple: Id={facture.Id}, FournisseurId={facture.FournisseurId}");
                        
                        _context.FacturesAchat.Add(facture);
                        try
                        {
                            await _context.SaveChangesAsync();
                            System.Diagnostics.Debug.WriteLine("Facture enregistrée avec succès");
                        }
                        catch (Exception ex)
                        {
                            // Capturer tous les détails de l'exception SQL
                            var fullError = $"Erreur lors de l'enregistrement de la facture: {ex.Message}";
                            var innerEx = ex.InnerException;
                            while (innerEx != null)
                            {
                                fullError += $"\nInner: {innerEx.GetType().Name} - {innerEx.Message}";
                                innerEx = innerEx.InnerException;
                            }
                            
                            System.Diagnostics.Debug.WriteLine(fullError);
                            throw new Exception($"Erreur SQL lors de l'enregistrement de la facture: {fullError}");
                        }
                        
                        // 4. Générer les IDs des lignes et les associer à la facture
                        foreach (var ligne in lignesFacture)
                        {
                            if (string.IsNullOrEmpty(ligne.Id))
                            {
                                ligne.Id = await GenerateUniqueLigneFactureIdAsync();
                            }
                            else
                            {
                                // Vérifier si l'ID existe déjà
                                bool idExists = await _context.LignesFacture.AnyAsync(l => l.Id == ligne.Id);
                                if (idExists)
                                {
                                    ligne.Id = await GenerateUniqueLigneFactureIdAsync();
                                }
                            }
                            
                            ligne.FactureId = facture.Id;
                            
                            // Vérifier si la ligne est déjà suivie
                            var existingLineEntry = _context.ChangeTracker.Entries<LigneFacture>()
                                .FirstOrDefault(e => e.Entity.Id == ligne.Id);
                                
                            if (existingLineEntry != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Détachement d'une ligne existante avec ID {ligne.Id}");
                                existingLineEntry.State = EntityState.Detached;
                            }
                            
                            // Vérifier la pièce
                            var piece = await _context.Pieces.FindAsync(ligne.PieceId);
                            if (piece == null)
                            {
                                throw new Exception($"La pièce {ligne.PieceId} n'existe pas");
                            }
                        }
                        
                        // 5. Ajouter les lignes une par une
                        foreach (var ligne in lignesFacture)
                        {
                            System.Diagnostics.Debug.WriteLine($"Ajout ligne: Id={ligne.Id}, PieceId={ligne.PieceId}, FactureId={ligne.FactureId}");
                            _context.LignesFacture.Add(ligne);
                            
                            try
                            {
                                await _context.SaveChangesAsync();
                                System.Diagnostics.Debug.WriteLine($"Ligne {ligne.Id} enregistrée avec succès");
                            }
                            catch (Exception ex)
                            {
                                var fullError = $"Erreur lors de l'enregistrement de la ligne {ligne.Id}: {ex.Message}";
                                var innerEx = ex.InnerException;
                                while (innerEx != null)
                                {
                                    fullError += $"\nInner: {innerEx.GetType().Name} - {innerEx.Message}";
                                    innerEx = innerEx.InnerException;
                                }
                                
                                System.Diagnostics.Debug.WriteLine(fullError);
                                throw new Exception($"Erreur SQL lors de l'enregistrement d'une ligne: {fullError}");
                            }
                            
                            // Créer le mouvement de stock et mettre à jour le stock
                            var piece = await _context.Pieces.FindAsync(ligne.PieceId);
                            
                            var mouvement = new MouvementStock
                            {
                                Id = await GenerateUniqueMouvementStockIdAsync(),
                                Date = DateTime.Now,
                                Type = "ENTREE", // ENTREE pour facture d'achat
                                Quantite = ligne.Quantite,
                                PieceId = ligne.PieceId,
                                FactureId = facture.Id
                            };
                            
                            _context.MouvementsStock.Add(mouvement);
                            
                            // Mettre à jour le stock (ajout pour achat)
                            piece.Stock += ligne.Quantite;
                            
                            // Sauvegarder le mouvement et la mise à jour du stock
                            try
                            {
                                await _context.SaveChangesAsync();
                                System.Diagnostics.Debug.WriteLine($"Mouvement de stock et mise à jour du stock pour {ligne.PieceId} réussis");
                            }
                            catch (Exception ex)
                            {
                                var fullError = $"Erreur lors de la mise à jour du stock: {ex.Message}";
                                var innerEx = ex.InnerException;
                                while (innerEx != null)
                                {
                                    fullError += $"\nInner: {innerEx.GetType().Name} - {innerEx.Message}";
                                    innerEx = innerEx.InnerException;
                                }
                                
                                System.Diagnostics.Debug.WriteLine(fullError);
                                throw new Exception($"Erreur SQL lors de la mise à jour du stock: {fullError}");
                            }
                        }
                        
                        // 6. Valider la transaction
                        await transaction.CommitAsync();
                        System.Diagnostics.Debug.WriteLine("Transaction validée avec succès");
                        
                        // 7. Réassocier les lignes à la facture pour le retour
                        facture.LignesFacture = lignesFacture;
                    }
                    catch (Exception ex)
                    {
                        // Annuler la transaction en cas d'erreur
                        await transaction.RollbackAsync();
                        System.Diagnostics.Debug.WriteLine($"Transaction annulée: {ex.Message}");
                        throw; // Relancer l'exception pour la capturer au niveau supérieur
                    }
                }
            }
            catch (Exception ex)
            {
                // Capturer tous les détails et construire un message complet
                var fullError = new System.Text.StringBuilder();
                fullError.AppendLine($"ERREUR COMPLÈTE: {ex.Message}");
                
                // Parcourir la chaîne d'exceptions internes
                var currentEx = ex;
                int level = 0;
                
                while (currentEx.InnerException != null)
                {
                    level++;
                    currentEx = currentEx.InnerException;
                    fullError.AppendLine($"NIVEAU {level}: [{currentEx.GetType().Name}] {currentEx.Message}");
                }
                
                fullError.AppendLine("STACK TRACE:");
                fullError.AppendLine(ex.StackTrace);
                
                // Afficher le message complet dans la console
                System.Diagnostics.Debug.WriteLine(fullError.ToString());
                
                // Relancer l'exception avec tous les détails
                throw new Exception($"Erreur lors de l'ajout de la facture: {ex.Message}", ex);
            }
        }
        
        // Méthodes auxiliaires pour générer des IDs uniques
        private async Task<string> GenerateUniqueFactureIdAsync()
        {
            string id;
            bool idExists;
            int attempts = 0;
            const int maxAttempts = 10;
            
            do
            {
                attempts++;
                id = _idGenerator.GenerateId(nameof(FactureAchat));
                idExists = await _context.FacturesAchat.AnyAsync(f => f.Id == id);
                
                if (idExists)
                {
                    System.Diagnostics.Debug.WriteLine($"ID {id} déjà existant, tentative {attempts}");
                    // Attendre un peu pour éviter les collisions avec le time-based ID
                    await Task.Delay(10);
                }
            } while (idExists && attempts < maxAttempts);
            
            if (idExists)
            {
                // Si après 10 essais on n'a pas d'ID unique, utiliser un GUID
                id = $"FA{DateTime.Now:yy}{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
                System.Diagnostics.Debug.WriteLine($"Utilisation d'un GUID pour l'ID: {id}");
            }
            
            return id;
        }
        
        private async Task<string> GenerateUniqueLigneFactureIdAsync()
        {
            string id;
            bool idExists;
            int attempts = 0;
            const int maxAttempts = 10;
            
            do
            {
                attempts++;
                id = _idGenerator.GenerateId(nameof(LigneFacture));
                idExists = await _context.LignesFacture.AnyAsync(l => l.Id == id);
                
                if (idExists)
                {
                    await Task.Delay(10);
                }
            } while (idExists && attempts < maxAttempts);
            
            if (idExists)
            {
                id = $"LF{DateTime.Now:yy}{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            }
            
            return id;
        }
        
        private async Task<string> GenerateUniqueMouvementStockIdAsync()
        {
            string id;
            bool idExists;
            int attempts = 0;
            const int maxAttempts = 10;
            
            do
            {
                attempts++;
                id = _idGenerator.GenerateId(nameof(MouvementStock));
                idExists = await _context.MouvementsStock.AnyAsync(m => m.Id == id);
                
                if (idExists)
                {
                    await Task.Delay(10);
                }
            } while (idExists && attempts < maxAttempts);
            
            if (idExists)
            {
                id = $"MS{DateTime.Now:yy}{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            }
            
            return id;
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