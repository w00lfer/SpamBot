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
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;

        public EmailService(IEmailRepository emailRepository, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEmailDto>> GetAllEmails() =>
            _mapper.Map<List<GetEmailDto>>(await _emailRepository.GetAllEmails().ToListAsync());

        public async Task<GetEmailDto> GetEmailByIdAsync(int id) =>
            _mapper.Map<GetEmailDto>(await _emailRepository.GetEmailByIdAsync(id));

        public async Task CreateEmailAsync(CreateEmailDto email) => 
            await _emailRepository.CreateEmailAsync(_mapper.Map<Email>(email));

        public Task UpdateEmailAsync(UpdateEmailDto email) =>
            _emailRepository.UpdateEmailAsync(_mapper.Map<Email>(email));

        public async Task DeleteEmailAsync(int id) => 
            await _emailRepository.DeleteEmailAsync(id);
    }
}
