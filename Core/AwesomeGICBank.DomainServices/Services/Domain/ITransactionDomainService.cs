using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public interface ITransactionDomainService
{
    TransactionId GenerateNextTransactionId(DateOnly date);
}
