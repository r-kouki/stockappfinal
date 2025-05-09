using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class FournisseurRepository : Repository<Fournisseur>, IFournisseurRepository
    {
        public FournisseurRepository(StockContext context) : base(context)
        {
        }

        public async Task<Fournisseur> GetWithFacturesAsync(Guid id)
        {
            return await _context.Fournisseurs
                .Include(f => f.FacturesAchat)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
} 