using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(StockContext context) : base(context)
        {
        }

        public async Task<Client> GetWithFacturesAsync(Guid id)
        {
            return await _context.Clients
                .Include(c => c.FacturesVente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
} 