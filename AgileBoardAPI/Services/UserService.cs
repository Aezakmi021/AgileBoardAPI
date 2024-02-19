using AgileBoardAPI.DTO;
using AgileBoardAPI.Interfaces;
using AgileBoardAPI.Models;
using AgileBoardAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AgileBoardAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _userRepository.ExistsByEmail(email);
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            return await _userRepository.ExistsByUsername(username);
        }

        public async Task<string> GetUsername()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            return username;
        }

        public async Task<User> SaveNewUser(RegistrationRequest registrationRequest)
        {
            var user = new User
            {
                Username = registrationRequest.Username,
                Email = registrationRequest.Email,
                // Set other properties
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (result.Succeeded)
            {
                // Additional logic if needed
                return user;
            }
            else
            {
                // Handle errors, maybe throw an exception or return null
                throw new Exception($"User creation failed: {string.Join(", ", result.Errors)}");
            }
        }

        public async Task<User> FindByUsername(string username)
        {
            // Implement logic to find user by username
            return await _userManager.FindByNameAsync(username);
        }

        public bool ValidatePassword(User user, string password)
        {
            // Implement logic to validate user password
            return _userManager.CheckPasswordAsync(user, password).Result;
        }

        public void EnableUser(User user)
        {
            // Implement logic to enable user account
            // Example: user.IsEnabled = true;
            // Save changes to the database
            _userRepository.Update(user);
        }

        public async Task<User> FindById(string userId)
        {
            // Implement logic to find user by ID
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            // Implement logic to check if username already exists
            var existingUser = await _userManager.FindByNameAsync(username);
            return existingUser != null;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            // Implement logic to check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            return existingUser != null;
        }
    }
}
