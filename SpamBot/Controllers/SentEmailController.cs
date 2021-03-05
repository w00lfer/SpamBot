using Microsoft.AspNetCore.Mvc;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentEmailController : ControllerBase
    {
        private readonly ISentEmailService _emailService;

        public SentEmailController(ISentEmailService emailService)
            => _emailService = emailService;

        [HttpGet]
        public async Task<List<GetSentEmailDto>> GetAllSentEmails()
           => await _emailService.GetAllSentEmails();

        [HttpGet]
        [Route("{id}")]
        public async Task<GetSentEmailDto> GetReceiverById(int id)
            => await _emailService.GetSentEmailByIdAsync(id);

        [HttpPut]
        public async Task UpdateSentEmail(UpdateSentEmailDto sentEmail)
            => await _emailService.UpdateSentEmailAsync(sentEmail);

        [HttpPost]
        public async Task CreateSentEmail(CreateSentEmailDto sentEmail)
            => await _emailService.CreateSentEmailAsync(sentEmail);
    }
}
