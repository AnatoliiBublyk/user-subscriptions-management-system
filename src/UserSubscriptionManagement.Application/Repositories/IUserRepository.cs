﻿using UserSubscriptionManagement.Domain.Models;

namespace UserSubscriptionManagement.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User> GetByUsernameAsync(string username);
}