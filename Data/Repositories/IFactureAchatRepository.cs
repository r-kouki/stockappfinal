using StockApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IFactureAchatRepository : IRepository<FactureAchat>
    {
        Task<FactureAchat> GetWithDetailsAsync(Guid id);
        Task<IEnumerable<FactureAchat>> GetAllWithDetailsAsync();
    }
} 