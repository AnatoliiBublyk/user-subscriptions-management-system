using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Entities;
using UserSubscriptionManagement.Infrastructure.Mapping;

namespace UserSubscriptionManagement.Infrastructure.Repository
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly UserSubscriptionsManagementContext _context;
        private readonly IMapper _mapper;
        private readonly MapsterConfig _mappingConfig;

        public UserSubscriptionRepository(UserSubscriptionsManagementContext context, IMapper mapper, MapsterConfig mappingConfig)
        {
            _context = context;
            _mapper = mapper;
            _mappingConfig = mappingConfig;
        }

        public async Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            var userSubscriptions = await _context.UserSubscriptions
                .Include(x => x.User)
                .Include(x => x.Subscription)
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserSubscription>>(userSubscriptions);
        }

        public async Task<IEnumerable<UserSubscription>> GetAllByUserIdAsync(int id)
        {
            var userSubscriptions = await _context.UserSubscriptions
                .Include(x => x.Subscription)
                .Where(x => x.UserId == id)
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserSubscription>>(userSubscriptions);
        }

        public async Task<IEnumerable<UserSubscription>> GetAllByUsernameAsync(string username)
        {
            var userSubscriptions = await _context.UserSubscriptions
                .Include(x => x.User)
                .Include(x => x.Subscription)
                .Where(x => x.User.Username == username)
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserSubscription>>(userSubscriptions);
        }

        public async Task AddUserSubscriptionAsync(UserSubscription userSubscription)
        {
            var entity = userSubscription.Adapt<UserSubscriptions>(_mappingConfig.Config);
            await _context.UserSubscriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserSubscriptionAsync(int userId, int subId)
        {
            var entity =
                await _context.UserSubscriptions.FirstOrDefaultAsync(x =>
                    x.UserId == userId && x.SubscriptionId == subId);
            if (entity is null)
                return;
            _context.UserSubscriptions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}