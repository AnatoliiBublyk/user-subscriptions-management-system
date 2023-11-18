using System.ComponentModel.DataAnnotations;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,user")]
    [Consumes("application/json")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _profileRepository;

        public UserProfileController(IMapper mapper, IUserProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<UserProfileDto> GetUserProfileByIdAsync(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);
            return _mapper.Map<UserProfileDto>(profile);
        }

        [HttpPut("{id}")]
        public async Task UpdateUserProfileAsync([Required] int id, [FromBody] UserProfileDto profile)
        {
            await _profileRepository.UpdateAsync(_mapper.Map<UserProfile>(profile));
        }
    }
}
