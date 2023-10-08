﻿namespace UserSubscriptionManagement.Application.Repositories;

public interface IBaseRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync();
    Task UpdateAsync();
    Task DeleteAsync();
}