using SpamBotApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories.Interfaces
{
    public interface ISentEmailRepository
    {
        IQueryable<SentEmail> GetAllSentEmails();
        Task<SentEmail> GetSentEmailByIdAsync(int id);
        Task CreateSentEmailAsync(SentEmail sentEmail);
        Task UpdateSentEmailAsync(SentEmail sentEmail);
        Task DeleteSentEmailAsync(int id);
    }
}
