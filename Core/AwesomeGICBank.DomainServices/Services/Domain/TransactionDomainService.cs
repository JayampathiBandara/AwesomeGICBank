using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class TransactionDomainService : ITransactionDomainService
{
    private readonly ITransactionRepository _transactionRepository;
    public TransactionDomainService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    }

    public TransactionId GenerateNextTransactionId(DateOnly date)
    {
        var latestNumber = _transactionRepository.GetMaximumTransactionNo(date);

        var nextNumber = (latestNumber is null) ? 1 : uint.Parse(latestNumber.Split("-")[1]) + 1;

        return new TransactionId(date, nextNumber);
    }
}
