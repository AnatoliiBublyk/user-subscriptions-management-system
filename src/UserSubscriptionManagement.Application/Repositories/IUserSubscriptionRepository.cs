using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface IUserSubscriptionRepository
{
    public Task<IEnumerable<UserSubscription>> GetAllAsync();
    public Task<IEnumerable<UserSubscription>> GetAllByUserIdAsync(int id);
    public Task<IEnumerable<UserSubscription>> GetAllByUsernameAsync(string username);
    public Task AddUserSubscriptionAsync(UserSubscription userSubscription);
    public Task RemoveUserSubscriptionAsync(int userId, int subId);
}