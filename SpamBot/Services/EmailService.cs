using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Repositories.Interfaces;
using SpamBotApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpamBotApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _userId;

        public EmailService(IReceiverRepository receiverRepository, IMapper mapper, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _configuration = configuration;
            _userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task SendEmailAsync(SendEmailDto sendEmailDto)
        {
            if (sendEmailDto.SendingDate.Subtract(TimeSpan.FromHours(2)) < DateTime.Now)
                throw new Exception("You can't send email to paste date time!");

            var receiver = await _receiverRepository.GetReceiverByIdAsync(sendEmailDto.ReceiverId);
            if (receiver == null || receiver.CreatorId != _userId)
                throw new Exception("There is no such user or you don't have access to it");

            var timeLeftToSend = TimeSpan.FromTicks(sendEmailDto.SendingDate.Subtract(DateTime.Now).Subtract(TimeSpan.FromHours(2)).Ticks);

            byte[] imageByteArray = default;
            if (sendEmailDto.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    await sendEmailDto.Image.CopyToAsync(ms);
                    imageByteArray = ms.ToArray();
                }
            }

            Task.Run(async () =>
            {
                await Task.Delay(timeLeftToSend);

                const string sender = "Excited User";
                const string senderEmail = "mailgun@testdomain.com";

                var content = new MultipartFormDataContent
                {
                    { new StringContent($"{sender} <{senderEmail}>"), "from" },
                    { new StringContent(receiver.Email), "to" },
                    { new StringContent(sendEmailDto.Title), "subject" },
                    { new StringContent($"{sendEmailDto.Description}"), "text" }
                };

                var resource = $"{_configuration.GetValue<string>("MailgunResource")}/messages";
                if (sendEmailDto.Image != null)
                {
                    content.Add(new ByteArrayContent(imageByteArray), "inline", sendEmailDto.Image.FileName);
                }
                await _httpClient.PostAsync(resource, content);
            });
        }
    }
}
