using AgileBoardAPI.Data;
using AgileBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardAPI.Repository
{
    public interface IConfirmationTokenRepository
    {
        Task<ConfirmationToken> GetById(Guid id);
        Task<ConfirmationToken> GetByToken(string token);
        Task Add(ConfirmationToken confirmationToken);
        Task Update(ConfirmationToken confirmationToken);
        Task Remove(ConfirmationToken confirmationToken);
    }

    public class ConfirmationTokenRepository : IConfirmationTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConfirmationTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ConfirmationToken> GetById(Guid id)
        {
            return await _dbContext.ConfirmationTokens.FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<ConfirmationToken> GetByToken(string token)
        {
            return await _dbContext.ConfirmationTokens.FirstOrDefaultAsync(ct => ct.Token == token);
        }

        public async Task Add(ConfirmationToken confirmationToken)
        {
            await _dbContext.ConfirmationTokens.AddAsync(confirmationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(ConfirmationToken confirmationToken)
        {
            _dbContext.ConfirmationTokens.Update(confirmationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(ConfirmationToken confirmationToken)
        {
            _dbContext.ConfirmationTokens.Remove(confirmationToken);
            await _dbContext.SaveChangesAsync();
        }
    }
}
