using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface IUserSubscriptionRepository
{
    public Task<UserSubscription> GetAllByUsernameAsync(string username);
}