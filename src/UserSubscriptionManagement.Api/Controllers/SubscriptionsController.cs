using System.ComponentModel.DataAnnotations;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionRepository subscriptionRepo, IMapper mapper)
        {
            _subscriptionRepo = subscriptionRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var result = await _subscriptionRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriptionDto>>(result);
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public async Task<SubscriptionDto> GetSubscriptionByIdAsync([Required] int id)
        {
            var result = await _subscriptionRepo.GetByIdAsync(id);
            return _mapper.Map<SubscriptionDto>(result);
        }

        [HttpGet("{key}")]
        [Produces("application/json")]
        public async Task<SubscriptionDto> GetSubscriptionByKeyAsync([Required] string key)
        {
            var result = await _subscriptionRepo.GetByKeyAsync(key);
            return _mapper.Map<SubscriptionDto>(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task AddSubscriptionAsync([FromBody] SubscriptionDto subscription)
        {
            var sub = _mapper.Map<Subscription>(subscription);
            await _subscriptionRepo.AddAsync(sub);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task UpdateSubscriptionAsync([Required] int id, [FromBody] SubscriptionDto subscription)
        {
            var sub = _mapper.Map<Subscription>(subscription);
            await _subscriptionRepo.UpdateAsync(sub);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task DeleteSubscription([Required] int id)
        {
            await _subscriptionRepo.DeleteAsync(id);
        }
    }
}
