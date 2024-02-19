using AgileBoardAPI.Data;
using AgileBoardAPI.Interfaces;
using AgileBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardAPI.Services
{
    public class UserPrincipalService : IUserPrincipalService
    {
        private readonly ApplicationDbContext _context; // Assuming ApplicationDbContext is your DbContext

        public UserPrincipalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> LoadUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new InvalidOperationException($"User not found: {username}");
            }
            return user; // You might need to convert this to a ClaimsPrincipal or a UserDetails object
        }
    }
}
