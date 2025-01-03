﻿using AwesomeGICBank.Domain.Entities;

namespace AwesomeGICBank.DomainServices.Services.Persistence;

public interface IAccountRepository
{
    public Task CreateAsync(Account account);
    public Task<Account> GetAsync(string accountNo);
    public Task<bool> ExistsAsync(string accountNo);
}