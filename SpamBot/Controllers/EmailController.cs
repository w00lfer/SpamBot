﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Services.Interfaces;
using System.Threading.Tasks;

namespace SpamBotApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
            => _emailService = emailService;

        [HttpPost]
        public async Task SendEmail(SendEmailDto email)
            => await _emailService.SendEmailAsync(email);

        [HttpPost("test/SendNonScheduledEmail")]
        public async Task SendNonScheduledEmail(SendEmailDto email)
            => await _emailService.SendNonScheduledEmailAsync(email);
    }
}
