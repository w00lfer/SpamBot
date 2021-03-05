using System;

namespace SpamBotApi.Models.Dtos
{
    public class GetSentEmailDto
    {
        public string ReceiverEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime SentDate { get; set; }
    }
}
