using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Domain.Interfaces;

public interface IBankStatementDomainService
{
    public Task<BankStatement> GenerateStatementAsync(
        string accountNo,
        int year,
        int month);
}
