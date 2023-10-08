using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface IUserSubscriptionRepository
{
    public Task<IEnumerable<UserSubscription>> GetAllByUsernameAsync(string username);
}