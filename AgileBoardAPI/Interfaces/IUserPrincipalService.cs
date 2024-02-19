using AgileBoardAPI.Models;

namespace AgileBoardAPI.Interfaces
{
    public interface IUserPrincipalService
    {
        Task<User> LoadUserByUsername(string username);
    }
}
