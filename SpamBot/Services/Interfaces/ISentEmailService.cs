using SpamBotApi.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Services.Interfaces
{
    public interface ISentEmailService
    {
        Task<List<GetSentEmailDto>> GetAllSentEmails();
        Task<GetSentEmailDto> GetSentEmailByIdAsync(int id);
        Task CreateSentEmailAsync(CreateSentEmailDto sentEmail);
        Task UpdateSentEmailAsync(UpdateSentEmailDto sentEmail);
        Task DeleteSentEmailAsync(int id);
    }
}
