using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Mapping;
using UserSubscriptionManagement.Infrastructure.Services.Interfaces;

namespace UserSubscriptionManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MapsterConfig _mappingConfig;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public UsersController(IMapper mapper, IUserRepository userRepository, IHashService hashService, MapsterConfig mappingConfig)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _hashService = hashService;
            _mappingConfig = mappingConfig;
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        [Produces("application/json")]
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin,user")]
        [Produces("application/json")]
        public async Task<UserDto> GetUserByIdAsync([Required] int id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task AddUserAsync([FromBody] UserInfoDto userInfo)
        {
            var user = userInfo.Adapt<User>(_mappingConfig.Config);
            user.PasswordHash = _hashService.GetHash(userInfo.Password);
            await _userRepository.AddAsync(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        [HttpPut("/add-balance")]
        [Authorize(Roles = "admin")]
        public async Task AddBalanceAsync([FromQuery] int id, [FromBody] decimal balance)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Balance += balance;
            await _userRepository.UpdateAsync(user);
        }
    }
}
