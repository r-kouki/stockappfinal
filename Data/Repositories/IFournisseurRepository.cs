using StockApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IFournisseurRepository : IRepository<Fournisseur>
    {
        Task<Fournisseur> GetWithFacturesAsync(Guid id);
    }
} 