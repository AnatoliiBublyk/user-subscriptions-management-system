using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Services.Interfaces;

namespace UserSubscriptionManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public AuthorizationController(IConfiguration configuration, IHashService hashService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _hashService = hashService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(LoginDto login)
        {
            var user = await _userRepository.GetByUsernameAsync(login.Username);
            if (user is null)
                return Unauthorized("Wrong username");
            if (!_hashService.VerifyHash(login.Password, user.PasswordHash))
                return Unauthorized("Wrong password");
            string token = GetToken(user);
            return Ok(token);
        }

        private string GetToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("TokenInfo:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration.GetSection("TokenInfo:Lifetime").Value)),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
