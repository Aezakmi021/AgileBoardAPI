using AgileBoardAPI.Models;

namespace AgileBoardAPI.Interfaces
{
    public interface IConfirmationTokenService
    {
        Task<ConfirmationToken> GenerateConfirmationToken(User user);
        Task<ConfirmationToken> GetByToken(string token);
        Task DeleteToken(ConfirmationToken confirmationToken);
    }
}
