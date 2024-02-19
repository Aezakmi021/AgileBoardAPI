using System.Threading.Tasks;
using AgileBoardAPI.DTO;

namespace AgileBoardAPI.Interfaces
{
    public interface IAuthService
    {
        Task SignUp(RegistrationRequest registrationRequest);
        string Login(LoginRequest loginRequest);
        void VerifyAccount(string token);
        void Logout();
    }
}
