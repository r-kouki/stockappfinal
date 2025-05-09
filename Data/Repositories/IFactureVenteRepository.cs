using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IFactureVenteRepository : IRepository<FactureVente>
    {
        Task<FactureVente> GetWithDetailsAsync(Guid id);
        Task<IEnumerable<FactureVente>> GetAllWithDetailsAsync();
    }
} 