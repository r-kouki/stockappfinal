using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace StockApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly StockContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IIdGeneratorService _idGenerator;

        public Repository(StockContext context, IIdGeneratorService idGenerator)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _idGenerator = idGenerator;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                // Générer l'ID si nécessaire
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null && idProperty.PropertyType == typeof(string))
                {
                    var currentId = idProperty.GetValue(entity) as string;
                    if (string.IsNullOrEmpty(currentId))
                    {
                        // Generate new ID based on entity type
                        var entityName = entity.GetType().Name;
                        var newId = _idGenerator.GenerateId(entityName);
                        idProperty.SetValue(entity, newId);
                    }
                }
                
                try
                {
                    // Détacher toute entité existante avec le même ID
                    if (idProperty != null)
                    {
                        var id = idProperty.GetValue(entity);
                        if (id != null)
                        {
                            var existingEntity = await _dbSet.FindAsync(id);
                            if (existingEntity != null)
                            {
                                _context.Entry(existingEntity).State = EntityState.Detached;
                            }
                        }
                    }
                    
                    // Tentative simple d'ajout
                    _context.ChangeTracker.Clear();
                    await _dbSet.AddAsync(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur d'ajout: {ex.Message} - {ex.InnerException?.Message}");
                    
                    // Essayer avec un nouvel ID
                    if (idProperty != null)
                    {
                        var entityName = entity.GetType().Name;
                        var uniqueId = _idGenerator.GenerateId(entityName) + "_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                        idProperty.SetValue(entity, uniqueId);
                        
                        _context.ChangeTracker.Clear();
                        await _dbSet.AddAsync(entity);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception dans AddAsync: {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                // Get the entity's ID through reflection
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    var id = idProperty.GetValue(entity);
                    
                    // Détacher toute entité existante
                    var existingEntity = await _dbSet.FindAsync(id);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).State = EntityState.Detached;
                    }
                    
                    // Clear tracking et attacher comme modifié
                    _context.ChangeTracker.Clear();
                    _context.Set<T>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    
                    // Sauvegarder les changements
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateAsync: {ex.Message}");
                throw;
            }
        }

        public virtual async Task DeleteAsync(string id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteAsync: {ex.Message}");
                throw;
            }
        }
    }
} 