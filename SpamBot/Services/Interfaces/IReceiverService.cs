using SpamBotApi.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Services.Interfaces
{
    public interface IReceiverService
    {
        Task<List<GetReceiverDto>> GetAllReceivers();
        Task<GetReceiverDto> GetReceiverByIdAsync(int id);
        Task CreateReceiverAsync(CreateReceiverDto receiver);
        Task UpdateReceiverAsync(UpdateReceiverDto receiver);
        Task DeleteReceiverAsync(int id);
    }
}
