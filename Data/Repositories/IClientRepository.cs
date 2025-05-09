using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetWithFacturesAsync(Guid id);
    }
} 