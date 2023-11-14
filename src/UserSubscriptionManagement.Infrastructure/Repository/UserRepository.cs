using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Entities;

namespace UserSubscriptionManagement.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserSubscriptionsManagementContext _context;
        private readonly IMapper _mapper;

        public UserRepository(UserSubscriptionsManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);   
            return _mapper.Map<User>(result ?? throw new ArgumentNullException($"User with id {id} not found"));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<User>>(result);
        }

        public async Task AddAsync(User user, UserProfile profile)
        {
           var entity = _mapper.Map<Users>(user);
           var entityProfile = _mapper.Map<UsersProfile>(profile);
           entity.Profile = entityProfile;
           await _context.Users.AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var entity = _mapper.Map<Users>(user);
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return false;
            var profileEntity = await _context.UsersProfile.FirstOrDefaultAsync(x => x.Id == entity.ProfileId);
            _context.Users.Remove(entity);
            if(profileEntity is not null) 
                _context.UsersProfile.Remove(profileEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
