using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGICBank.SqlServerPersistence.Repositories;

public class InterestRuleRepository :
  BaseRepository, IInterestRuleRepository
{
    public InterestRuleRepository(AwesomeGICBankDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateOrUpdateAsync(InterestRule interestRule)
    {
        var rule = await _dbContext
            .InterestRules
            .FirstOrDefaultAsync(i => i.Date == interestRule.Date);

        if (rule == null)
        {
            await _dbContext.Set<InterestRule>().AddAsync(interestRule);
        }
        else
        {
            rule.Rate = interestRule.Rate;
            rule.RuleId = interestRule.RuleId;
        }
    }

    public async Task<List<InterestRule>> GetAllAsync()
    {
        return await _dbContext
            .Set<InterestRule>()
            .ToListAsync();
    }

    public Task<int> GetMaximumSequenceNoAsync(DateOnly transactionDate)
    {
        return Task.FromResult(2);
    }
}