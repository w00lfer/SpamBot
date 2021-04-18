using AutoMapper;
using Microsoft.Extensions.Configuration;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Repositories.Interfaces;
using SpamBotApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SpamBotApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IEmailRepository emailRepository, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(SendEmailDto sendEmailDto)
        {
            if (sendEmailDto.SendingDate < DateTime.Now)
                throw new Exception("You can't send email to paste date time!");
            
            

            if (string.IsNullOrEmpty(sendEmailDto.Image))
            {
                await SendEmailViaMailgun(sendEmailDto.ReceiverEmail, sendEmailDto.Title, sendEmailDto.Description, sendEmailDto.SendingDate);
            }
            else
            {
                var imageDataByteArray = Convert.FromBase64String(sendEmailDto.Image);
                await SendEmailViaMailgun(sendEmailDto.ReceiverEmail, sendEmailDto.Title, sendEmailDto.Description, sendEmailDto.SendingDate, imageDataByteArray);
            }
        }

        private async Task SendEmailViaMailgun(string receiverEmail, string title, string description, DateTime sendingDate, byte[] image = null)
        {
            var timeLeftToSend = TimeSpan.FromTicks(sendingDate.Subtract(DateTime.Now).Ticks);

            Task.Run(async () =>
            {
                await Task.Delay(timeLeftToSend);
                const string sender = "Excited User";
                const string senderEmail = "mailgun@testdomain.com";
                var emailDicitonary = new Dictionary<string, string>
            {
                { "from", $"{sender} <{senderEmail}>" },
                { "to", receiverEmail },
                { "subject", title },
                { "text", description },
            };

                if (image != null)
                    emailDicitonary.Add("inline", image.ToString());

                var formContent = new FormUrlEncodedContent(emailDicitonary);

                var resource = $"{_configuration.GetValue<string>("MailgunResource")}/messages";
                var result = await _httpClient.PostAsync(resource, formContent);
            });

        }
    }
}
