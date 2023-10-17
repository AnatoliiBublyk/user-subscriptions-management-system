using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Database;

namespace UserSubscriptionManagement.Infrastructure.Repository
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly UserSubscriptionsManagementContext _context;
        private readonly IMapper _mapper;

        public UserSubscriptionRepository(UserSubscriptionsManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserSubscription>> GetAllByUsernameAsync(string username)
        {
            var userSubscriptions = await _context.UserSubscriptions.Include(x => x.User).Where(x => x.User.Username == username).ToListAsync();
            return _mapper.Map<IEnumerable<UserSubscription>>(userSubscriptions);
        }
    }
}