using StockApp.Data.Entities;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string username, string password);
        Task<User> FindByUsernameAsync(string username);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<User> DirectLoginAsync(string username, string password);
    }
} 