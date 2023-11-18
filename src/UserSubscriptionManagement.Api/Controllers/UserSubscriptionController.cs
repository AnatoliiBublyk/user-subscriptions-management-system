using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionManagement.Application.Services.Interfaces;
using UserSubscriptionManagement.Contracts.Responses;

namespace UserSubscriptionManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionService _userSubscriptionService;


        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public async Task<IEnumerable<UserSubscriptionsResponse>> GetAllUsersSubscriptionsAsync()
        {
            var res = await _userSubscriptionService.GetAllUsersSubscriptionsAsync();
            return res;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        [Produces("application/json")]
        public async Task<UserSubscriptionsResponse> GetUserSubscriptionsAsync(int id)
        {
            var res = await _userSubscriptionService.GetUserSubscriptionsAsync(id);
            return res;
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task AddUserSubscriptionAsync([FromQuery] int userId, [FromQuery] string subscriptionKey)
        {
            await _userSubscriptionService.AddUserSubscriptionAsync(userId, subscriptionKey);
        }

        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task RemoveUserSubscriptionAsync([FromQuery] int userId, [FromQuery] string subscriptionKey)
        {
            await _userSubscriptionService.RemoveUserSubscriptionAsync(userId, subscriptionKey);
        }
    }
}
