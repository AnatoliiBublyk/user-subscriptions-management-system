using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Domain.Models;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Entities;

namespace UserSubscriptionManagement.Infrastructure.Repository;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly UserSubscriptionsManagementContext _context;
    private readonly IMapper _mapper;

    public SubscriptionRepository(UserSubscriptionsManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Subscription> GetByIdAsync(int id)
    {
        var result = await _context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<Subscription>(result ?? throw new ArgumentNullException($"No subscription with id {id} not found"));
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        var result = await _context.Subscriptions.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<Subscription>>(result);
    }

    public async Task AddAsync(Subscription subscription)
    {
        var entity = _mapper.Map<Subscriptions>(subscription);
        await _context.Subscriptions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        var entity = _mapper.Map<Subscriptions>(subscription);
        _context.Subscriptions.Update(entity);
        await _context.SaveChangesAsync();

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return false;
        _context.Subscriptions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Subscription> GetByKeyAsync(string key)
    {
        var result = await _context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(x => x.Key == key);
        return _mapper.Map<Subscription>(result ?? throw new ArgumentNullException($"No subscription with key {key} found"));

    }
}