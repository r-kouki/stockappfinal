using Microsoft.EntityFrameworkCore;
using StockApp.Data.Entities;
using System.Threading.Tasks;

namespace StockApp.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(StockContext context) : base(context)
        {
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            // In a real application, you would hash the password
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password && u.Actif);
        }
    }
} 