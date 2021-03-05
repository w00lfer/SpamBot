using Microsoft.EntityFrameworkCore;
using SpamBotApi.Models;
using SpamBotApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories
{
    public class SentEmailRepository : ISentEmailRepository
    {
        private readonly AppDbContext _appDbContext;

        public SentEmailRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<SentEmail> GetAllSentEmails() => _appDbContext.SentEmails
            .AsNoTracking();

        public async Task<SentEmail> GetSentEmailByIdAsync(int id) => await _appDbContext.SentEmails
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        public async Task CreateSentEmailAsync(SentEmail sentEmail)
        {
            await _appDbContext.SentEmails.AddAsync(sentEmail);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateSentEmailAsync(SentEmail sentEmail)
        {
            _appDbContext.SentEmails.Update(sentEmail);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSentEmailAsync(int id)
        {
            _appDbContext.SentEmails.Remove(await GetSentEmailByIdAsync(id));
            await _appDbContext.SaveChangesAsync();
        }
    }
}
