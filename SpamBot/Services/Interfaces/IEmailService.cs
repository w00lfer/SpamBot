using SpamBotApi.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task<List<GetEmailDto>> GetAllEmails();
        Task<GetEmailDto> GetEmailByIdAsync(int id);
        Task CreateEmailAsync(CreateEmailDto email);
        Task UpdateEmailAsync(UpdateEmailDto email);
        Task DeleteEmailAsync(int id);
    }
}
