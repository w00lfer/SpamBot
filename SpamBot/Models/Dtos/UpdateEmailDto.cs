namespace SpamBotApi.Models.Dtos
{
    public class UpdateEmailDto
    {
        public string ReceiverEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
