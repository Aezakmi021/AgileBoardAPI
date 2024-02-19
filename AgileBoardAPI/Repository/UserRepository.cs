using AgileBoardAPI.Data;
using AgileBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task<bool> ExistsByEmail(string email);
        Task<User> Update(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> Update(User user)
        {
            // Implement logic to update user in the database
            // Example:
            var existingUser = await _dbContext.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                // Update user properties
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.IsEnabled = user.IsEnabled;

                await _dbContext.SaveChangesAsync();
            }

            return existingUser;
        }
    }
}
