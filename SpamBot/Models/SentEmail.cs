using System;

namespace SpamBotApi.Models
{
    public class SentEmail
    {
        public int Id { get; set; }
        public string ReceiverEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime SentDate { get; set; }
    }
}
