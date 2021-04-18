using SpamBotApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;

namespace SpamBotApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<JwtToken> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                return GetTokenForSignedInUser(user);
            else
                throw new Exception("failed to create user");
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                    throw new Exception("Couldn't delete user");
            }
            else
                throw new Exception("Such user doesn't exist");
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task<JwtToken> LoginUserAsync(LoginDto loginDto) => await _userManager.FindByNameAsync(loginDto.Username) is IdentityUser user ?
            (await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, true)).Succeeded ?
                GetTokenForSignedInUser(user) :
                throw new Exception("Password is incorrect") :
            throw new Exception("Such a user with this username doesn't exist");

        private JwtToken GetTokenForSignedInUser(IdentityUser user)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var authSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecret")));

            var token = new JwtSecurityToken(
                issuer: "http://dotnetdetail.net",
                audience: "http://dotnetdetail.net",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo
            };
        }
    }
}
