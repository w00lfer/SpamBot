using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;
using System.Threading.Tasks;

namespace SpamBotApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<JwtToken> CreateUserAsync(RegisterDto registerDto);
        Task<JwtToken> LoginUserAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task DeleteUserAsync(string userId);
    }
}
