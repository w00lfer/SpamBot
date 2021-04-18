using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Repositories.Interfaces;
using SpamBotApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpamBotApi.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;
        private readonly string _userId;

        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
            _userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<List<GetReceiverDto>> GetAllReceivers()
        {
            var result = await _receiverRepository.GetAllReceivers().Where(r => r.CreatorId == _userId).ToListAsync();
            return _mapper.Map<List<GetReceiverDto>>(result);
        }

        public async Task<GetReceiverDto> GetReceiverByIdAsync(int id)
        {
            var result = await _receiverRepository.GetReceiverByIdAsync(id);
            if (result == null || result.CreatorId != _userId)
                throw new Exception("there is no such user or you don't have access to it");

            return _mapper.Map<GetReceiverDto>(result);
        }

        public async Task CreateReceiverAsync(CreateReceiverDto receiver)
        {
            var mappedReceiver = _mapper.Map<Receiver>(receiver);
            mappedReceiver.CreatorId = _userId;
            await _receiverRepository.CreateReceiverAsync(mappedReceiver);
        }

        public async Task UpdateReceiverAsync(UpdateReceiverDto receiver)
        {
            var receiverFromDb = await _receiverRepository.GetReceiverByIdAsync(receiver.Id);
            if (receiverFromDb == null || receiverFromDb.CreatorId != _userId)
                throw new Exception("There is no such receiver or you don't have access to it");

            await _receiverRepository.UpdateReceiverAsync(_mapper.Map<Receiver>(receiver));
        }

        public async Task DeleteReceiverAsync(int id)
        {
            var receiverFromDb = await _receiverRepository.GetReceiverByIdAsync(id);
            if (receiverFromDb == null || receiverFromDb.CreatorId != _userId)
                throw new Exception("There is no such receiver or you don't have access to it");

            await _receiverRepository.DeleteReceiverAsync(id);
        }
    }
}
