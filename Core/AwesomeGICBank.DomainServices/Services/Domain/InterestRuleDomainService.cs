using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class InterestRuleDomainService : IInterestRuleDomainService
{
    private readonly IUnitOfWork _unitOfWork;
    public InterestRuleDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<int> GenerateNextRuleSequenceNoAsync(DateOnly date)
    {
        var latestNumber = await _unitOfWork.InterestRuleRepository.GetMaximumSequenceNoAsync(date);

        return ++latestNumber;
    }
}