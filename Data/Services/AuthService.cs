using StockApp.Data.Entities;
using StockApp.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace StockApp.Data.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterUserAsync(User user);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            return await _userRepository.LoginAsync(username, password);
        }
        
        public async Task<User> RegisterUserAsync(User user)
        {
            // Check if username already exists
            var existingUser = await _userRepository.FindByUsernameAsync(user.Username);
            if (existingUser != null)
                throw new Exception("Username already exists");
                
            // Set default values
            user.Actif = true;
            user.CreatedAt = DateTime.Now;
            
            // Add user
            await _userRepository.AddAsync(user);
            return user;
        }
        
        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            return await _userRepository.ChangePasswordAsync(userId, currentPassword, newPassword);
        }
    }
} 