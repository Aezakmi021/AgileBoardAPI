
using AgileBoardAPI.DTO;
using AgileBoardAPI.Models;

namespace AgileBoardAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByUsername(string username);
        Task<User> SaveNewUser(RegistrationRequest registrationRequest);
        User FindByUsername(string username);
        bool ValidatePassword(User user, string password);
        void EnableUser(User user);
        Task<User> FindById(string userId);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfEmailExists(string email);
    }
}
