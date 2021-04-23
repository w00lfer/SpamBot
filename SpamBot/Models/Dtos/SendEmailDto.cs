using Microsoft.AspNetCore.Http;
using System;

namespace SpamBotApi.Models.Dtos
{
    public class SendEmailDto
    {
        public int ReceiverId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public DateTime SendingDate { get; set; }
    }
}
