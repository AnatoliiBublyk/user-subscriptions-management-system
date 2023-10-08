using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    public Task<Subscription> GetByKeyAsync(string key);
}