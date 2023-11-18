using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface IUserProfileRepository 
{
    Task<UserProfile> GetByIdAsync(int id);
    Task UpdateAsync(UserProfile userProfile);
}