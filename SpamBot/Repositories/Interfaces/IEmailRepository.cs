using SpamBotApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        IQueryable<Email> GetAllEmails();
        Task<Email> GetEmailByIdAsync(int id);
        Task CreateEmailAsync(Email email);
        Task UpdateEmailAsync(Email email);
        Task DeleteEmailAsync(int id);
    }
}
