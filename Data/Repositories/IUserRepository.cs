using StockApp.Data.Entities;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string username, string password);
    }
} 