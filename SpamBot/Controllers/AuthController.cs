using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Services.Interfaces;
using System.Threading.Tasks;

namespace SpamBotApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto) =>
            Ok(await _authService.CreateUserAsync(registerDto));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto) =>
            Ok(await _authService.LoginUserAsync(loginDto));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok();
        }
    }
}
