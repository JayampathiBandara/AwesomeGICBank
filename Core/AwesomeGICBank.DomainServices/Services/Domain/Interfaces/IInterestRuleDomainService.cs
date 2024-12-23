namespace AwesomeGICBank.DomainServices.Services.Domain.Interfaces;

public interface IInterestRuleDomainService
{
    Task<int> GenerateNextRuleSequenceNoAsync(DateOnly date);
}
