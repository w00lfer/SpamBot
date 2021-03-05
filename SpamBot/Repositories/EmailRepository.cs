using Microsoft.EntityFrameworkCore;
using SpamBotApi.Models;
using SpamBotApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmailRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<Email> GetAllEmails() => _appDbContext.Emails
            .AsNoTracking();

        public async Task<Email> GetEmailByIdAsync(int id) => await _appDbContext.Emails
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        public async Task CreateEmailAsync(Email email)
        {
            await _appDbContext.Emails.AddAsync(email);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateEmailAsync(Email email)
        {
            _appDbContext.Emails.Update(email);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteEmailAsync(int id)
        {
            _appDbContext.Emails.Remove(await GetEmailByIdAsync(id));
            await _appDbContext.SaveChangesAsync();
        }
    }
}
