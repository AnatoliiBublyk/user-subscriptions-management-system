using System.ComponentModel.DataAnnotations;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptions()
        {
            var result = await _subscriptionRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriptionDto>>(result);
        }

        [HttpGet("{id:int}")]
        public async Task<SubscriptionDto> GetSubscriptionById([Required] int id)
        {
            var result = await _subscriptionRepo.GetByIdAsync(id);
            return _mapper.Map<SubscriptionDto>(result);
        }

        [HttpGet("{key}")]
        public async Task<SubscriptionDto> GetSubscriptionByKey([Required] string key)
        {
            var result = await _subscriptionRepo.GetByKeyAsync(key);
            return _mapper.Map<SubscriptionDto>(result);
        }

        [HttpPost]
        public async Task AddSubscription([FromBody] SubscriptionDto subscription)
        {
            var sub = _mapper.Map<Subscription>(subscription);
            await _subscriptionRepo.AddAsync(sub);
        }

        [HttpPut("{id}")]
        public async Task UpdateSubscription([Required] int id, [FromBody] SubscriptionDto subscription)
        {
            var sub = _mapper.Map<Subscription>(subscription);
            await _subscriptionRepo.UpdateAsync(sub);
        }

        [HttpDelete("{id}")]
        public async Task DeleteSubscription([Required] int id)
        {
            await _subscriptionRepo.DeleteAsync(id);
        }
    }
}
