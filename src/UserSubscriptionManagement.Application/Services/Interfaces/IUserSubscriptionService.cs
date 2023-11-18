using UserSubscriptionManagement.Contracts.Responses;

namespace UserSubscriptionManagement.Application.Services.Interfaces;

public interface IUserSubscriptionService
{
    public Task<IEnumerable<UserSubscriptionsResponse>> GetAllUsersSubscriptionsAsync();
    public Task<UserSubscriptionsResponse> GetUserSubscriptionsAsync(int id);
    public Task AddUserSubscriptionAsync(int userId, string subscriptionKey);
    public Task RemoveUserSubscriptionAsync(int userId, string subscriptionKey);
    public Task PerformAutomaticWithdrawalAsync();
}