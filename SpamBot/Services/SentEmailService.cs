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
    public class SentEmailService : ISentEmailService
    {
        private readonly ISentEmailRepository _sentEmailRepository;
        private readonly IMapper _mapper;

        public SentEmailService(ISentEmailRepository sentEmailRepository, IMapper mapper)
        {
            _sentEmailRepository = sentEmailRepository;
            _mapper = mapper;
        }

        public async Task<List<GetSentEmailDto>> GetAllSentEmails() =>
            _mapper.Map<List<GetSentEmailDto>>(await _sentEmailRepository.GetAllSentEmails().ToListAsync());

        public async Task<GetSentEmailDto> GetSentEmailByIdAsync(int id) =>
            _mapper.Map<GetSentEmailDto>(await _sentEmailRepository.GetSentEmailByIdAsync(id));

        public async Task CreateSentEmailAsync(CreateSentEmailDto sentEmail) =>
            await _sentEmailRepository.CreateSentEmailAsync(_mapper.Map<SentEmail>(sentEmail));

        public Task UpdateSentEmailAsync(UpdateSentEmailDto sentEmail) =>
            _sentEmailRepository.UpdateSentEmailAsync(_mapper.Map<SentEmail>(sentEmail));

        public async Task DeleteSentEmailAsync(int id) =>
            await _sentEmailRepository.DeleteSentEmailAsync(id);
    }
}
