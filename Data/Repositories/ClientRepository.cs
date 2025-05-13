using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(StockContext context, IIdGeneratorService idGenerator) 
            : base(context, idGenerator)
        {
        }

        public async Task<Client> GetWithFacturesAsync(string id)
        {
            return await _context.Clients
                .Include(c => c.FacturesVente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
} 