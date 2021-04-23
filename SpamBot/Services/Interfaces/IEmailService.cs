using SpamBotApi.Models.Dtos;
using System.Threading.Tasks;

namespace SpamBotApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendEmailDto sendEmailDto);
        Task SendNonScheduledMailAsync(SendEmailDto sendEmailDto);
    }
}
