using AgileBoardAPI.Interfaces;
using AgileBoardAPI.Models;
using AgileBoardAPI.Repository;

namespace AgileBoardAPI.Services
{
    public class ConfirmationTokenService : IConfirmationTokenService
    {
        private readonly IConfirmationTokenRepository _confirmationTokenRepository;

        public ConfirmationTokenService(IConfirmationTokenRepository confirmationTokenRepository)
        {
            _confirmationTokenRepository = confirmationTokenRepository;
        }

        public async Task<ConfirmationToken> GenerateConfirmationToken(User user)
        {
            // Generate new token
            ConfirmationToken confirmationToken = new ConfirmationToken
            {
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                User = user
            };

            // Save token
            await _confirmationTokenRepository.Add(confirmationToken);
            return confirmationToken;
        }

        public async Task<ConfirmationToken> GetByToken(string token)
        {
            return await _confirmationTokenRepository.GetByToken(token);
        }

        public async Task DeleteToken(ConfirmationToken confirmationToken)
        {
            await _confirmationTokenRepository.Remove(confirmationToken);
        }
    }
}
