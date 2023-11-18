using System.Security.Cryptography.X509Certificates;
using MapsterMapper;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Application.Services.Interfaces;
using UserSubscriptionManagement.Contracts.Dtos;
using UserSubscriptionManagement.Contracts.Responses;
using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Services;

public class UserSubscriptionService : IUserSubscriptionService
{
    private readonly IMapper _mapper;
    private readonly IUserSubscriptionRepository _userSubscriptionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public UserSubscriptionService(IMapper mapper, IUserSubscriptionRepository userSubscriptionRepository, IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
    {
        _mapper = mapper;
        _userSubscriptionRepository = userSubscriptionRepository;
        _userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<UserSubscriptionsResponse>> GetAllUsersSubscriptionsAsync()
    {
        var usersSubs = await _userSubscriptionRepository.GetAllAsync();
        var result = usersSubs.GroupBy(x => x.User.Username).Select(g => new UserSubscriptionsResponse()
        {
            Username = g.Key,
            Subscriptions = _mapper.Map<IEnumerable<SubscriptionDto>>(g.Select(x => x.Subscription).ToList())
        });
        return result;
    }

    public async Task<UserSubscriptionsResponse> GetUserSubscriptionsAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        var userSubs = await _userSubscriptionRepository.GetAllByUserIdAsync(id);
        var result = new UserSubscriptionsResponse()
        {
            Username = user.Username,
            Subscriptions = _mapper.Map<IEnumerable<SubscriptionDto>>(userSubs.Select(s => s.Subscription))
        };
        return result;
    }

    public async Task AddUserSubscriptionAsync(int userId, string subscriptionKey)
    {
        var user = await _userRepository.GetByIdAsync(userId) 
                   ?? throw new ArgumentNullException($"User with id {userId} not found");
        var sub = await _subscriptionRepository.GetByKeyAsync(subscriptionKey) 
                  ?? throw new ArgumentNullException($"Subscription with key {subscriptionKey} not found");

        var userSub = new UserSubscription()
        {
            StartDate = DateTime.UtcNow,
            User = user,
            Subscription = sub
        };
        await _userSubscriptionRepository.AddUserSubscriptionAsync(userSub);
    }

    public async Task RemoveUserSubscriptionAsync(int userId, string subscriptionKey)
    {
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new ArgumentNullException($"User with id {userId} not found");
        var sub = await _subscriptionRepository.GetByKeyAsync(subscriptionKey)
                  ?? throw new ArgumentNullException($"Subscription with key {subscriptionKey} not found");

        await _userSubscriptionRepository.RemoveUserSubscriptionAsync(user.Id, sub.Id);
    }

    public async Task PerformAutomaticWithdrawalAsync()
    {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users)
        {
            var balance = user.Balance;
            var subs = await _userSubscriptionRepository.GetAllByUserIdAsync(user.Id);
            var relevantSubs = subs.Where(s =>
            {
                Console.WriteLine((DateTime.UtcNow - s.StartDate).Days);
                return (DateTime.UtcNow - s.StartDate).Days % s.Subscription.Duration == 0;
            }).ToList();
            if(!relevantSubs.Any())
                continue;
            var sum = relevantSubs.Sum(s => s.Subscription.Price);
            if (sum > balance)
            {
                foreach (var sub in relevantSubs)
                {
                    await RemoveUserSubscriptionAsync(user.Id, sub.Subscription.Key);
                }
            }
            else
            {
                user.Balance = balance - sum;
                await _userRepository.UpdateAsync(user);
            }
        }

    }
}