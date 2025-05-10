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
                // Set a custom ID if not set already
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
                
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddAsync: {ex.Message}");
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
                    
                    // Create a new context scope to avoid tracking conflicts
                    using (var scope = Program.ServiceProvider.CreateScope())
                    {
                        var newContext = scope.ServiceProvider.GetRequiredService<StockContext>();
                        
                        // Find entity with tracking
                        var existingEntity = await newContext.Set<T>().FindAsync(id);
                        if (existingEntity != null)
                        {
                            // Detach the existing entity
                            newContext.Entry(existingEntity).State = EntityState.Detached;
                        }
                        
                        // Attach and mark as modified
                        newContext.Set<T>().Attach(entity);
                        newContext.Entry(entity).State = EntityState.Modified;
                        
                        // Save changes in the new context
                        await newContext.SaveChangesAsync();
                        
                        // Detach in the original context if it exists there
                        var existingInOriginal = await _dbSet.FindAsync(id);
                        if (existingInOriginal != null)
                        {
                            _context.Entry(existingInOriginal).State = EntityState.Detached;
                        }
                    }
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