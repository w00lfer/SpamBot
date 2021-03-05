using Microsoft.EntityFrameworkCore;
using SpamBotApi.Models;
using SpamBotApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SpamBotApi.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReceiverRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<Receiver> GetAllReceivers() => _appDbContext.Receivers
           .AsNoTracking();

        public async Task<Receiver> GetReceiverByIdAsync(int id) => await _appDbContext.Receivers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        public async Task CreateReceiverAsync(Receiver receiver)
        {
            await _appDbContext.Receivers.AddAsync(receiver);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateReceiverAsync(Receiver Receiver)
        {
            _appDbContext.Receivers.Update(Receiver);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteReceiverAsync(int id)
        {
            _appDbContext.Receivers.Remove(await GetReceiverByIdAsync(id));
            await _appDbContext.SaveChangesAsync();
        }
    }
}
