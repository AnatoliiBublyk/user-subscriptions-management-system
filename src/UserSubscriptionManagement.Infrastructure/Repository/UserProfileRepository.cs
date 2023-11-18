using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Entities;

namespace UserSubscriptionManagement.Infrastructure.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserSubscriptionsManagementContext _context;
        private readonly IMapper _mapper;

        public UserProfileRepository(UserSubscriptionsManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            var result = await _context.UsersProfile.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);   
            return _mapper.Map<UserProfile>(result ?? throw new ArgumentNullException($"User profile with id {id} not found"));
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            var entity = _mapper.Map<UsersProfile>(userProfile);
            _context.UsersProfile.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
