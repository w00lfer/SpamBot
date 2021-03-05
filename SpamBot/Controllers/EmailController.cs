using Microsoft.AspNetCore.Mvc;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
            => _emailService = emailService;

        [HttpGet]
        public async Task<List<GetEmailDto>> GetAllEmails()
           => await _emailService.GetAllEmails();

        [HttpGet]
        [Route("{id}")]
        public async Task<GetEmailDto> GetReceiverById(int id)
            => await _emailService.GetEmailByIdAsync(id);

        [HttpPut]
        public async Task UpdateEmail(UpdateEmailDto email)
            => await _emailService.UpdateEmailAsync(email);

        [HttpPost]
        public async Task CreateEmail(CreateEmailDto email)
            => await _emailService.CreateEmailAsync(email);
    }
}
