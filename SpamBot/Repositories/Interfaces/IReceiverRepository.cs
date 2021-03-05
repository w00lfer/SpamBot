using SpamBotApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories.Interfaces
{
    public interface IReceiverRepository
    {
        IQueryable<Receiver> GetAllReceivers();
        Task<Receiver> GetReceiverByIdAsync(int id);
        Task CreateReceiverAsync(Receiver receiver);
        Task UpdateReceiverAsync(Receiver Receiver);
        Task DeleteReceiverAsync(int id);
    }
}
