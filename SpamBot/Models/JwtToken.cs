using System;

namespace SpamBotApi.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
