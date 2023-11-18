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
    public class UserRepository : IUserRepository
    {
        private readonly UserSubscriptionsManagementContext _context;
        private readonly IMapper _mapper;
        private readonly MapsterConfig _mappingConfig;

        public UserRepository(UserSubscriptionsManagementContext context, IMapper mapper, MapsterConfig mappingConfig)
        {
            _context = context;
            _mapper = mapper;
            _mappingConfig = mappingConfig;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users
                .Include(x => x.AdminType)
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                         ?? throw new ArgumentNullException($"User with id {id} not found");
            var user = result.Adapt<User>(_mappingConfig.Config);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users
                .Include(x => x.AdminType)
                .Include(x => x.Profile)
                .AsNoTracking()
                .ToListAsync();
            var users = result.Adapt<IEnumerable<User>>(_mappingConfig.Config);
            return users;
        }

        public async Task AddAsync(User user)
        {
            var entity = user.Adapt<Users>(_mappingConfig.Config);
            entity.AdminTypeId = (await _context.AdminTypes.FirstOrDefaultAsync(x => x.AdminType == user.Role)).Id;
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var entity = user.Adapt<Users>(_mappingConfig.Config);
            entity.AdminTypeId = (await _context.AdminTypes.FirstOrDefaultAsync(x => x.AdminType == user.Role)).Id;
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null) return false;
            var profileEntity = await _context.UsersProfile.FirstOrDefaultAsync(x => x.Id == entity.ProfileId);
            _context.Users.Remove(entity);
            if (profileEntity is not null)
                _context.UsersProfile.Remove(profileEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var result = await _context.Users
                             .Include(x => x.AdminType)
                             .Include(x => x.Profile)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.Username == username)
                         ?? throw new ArgumentNullException($"User with username {username} not found");
            var user = result.Adapt<User>(_mappingConfig.Config);
            return user;
        }
    }
}
