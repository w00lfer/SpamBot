using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Repositories.Interfaces;
using SpamBotApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
        }
        public async Task<List<GetReceiverDto>> GetAllReceivers() =>
            _mapper.Map<List<GetReceiverDto>>(await _receiverRepository.GetAllReceivers().ToListAsync());

        public async Task<GetReceiverDto> GetReceiverByIdAsync(int id) =>
            _mapper.Map<GetReceiverDto>(await _receiverRepository.GetReceiverByIdAsync(id));

        public async Task CreateReceiverAsync(CreateReceiverDto receiver) => 
            await _receiverRepository.CreateReceiverAsync(_mapper.Map<Receiver>(receiver));

        public async Task UpdateReceiverAsync(UpdateReceiverDto receiver) =>
            await _receiverRepository.UpdateReceiverAsync(_mapper.Map<Receiver>(receiver));

        public async Task DeleteReceiverAsync(int id) =>
            await _receiverRepository.DeleteReceiverAsync(id);
    }
}
