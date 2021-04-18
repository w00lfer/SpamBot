using System;

namespace SpamBotApi.Models.Dtos
{
    public class SendEmailDto
    {
        public string ReceiverEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime SendingDate { get; set; }
    }
}
