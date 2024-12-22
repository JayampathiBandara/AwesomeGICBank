using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class TransactionDomainService : ITransactionDomainService
{
    private readonly IUnitOfWork _unitOfWork;
    public TransactionDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TransactionId> GenerateNextTransactionId(DateOnly date)
    {
        var latestNumber = await _unitOfWork.TransactionRepository.GetMaximumTransactionIdAsync(date);

        var nextNumber = (latestNumber is null) ? 1 : uint.Parse(latestNumber.Split("-")[1]) + 1;

        return new TransactionId(date, nextNumber);
    }

    public async Task<Transaction> CreateTransactionAsync(
        string transactionType,
        string amount,
        string transactionDate)
    {
        var date = DateTimeHelpers.ConvertDateStringToDateOnly(transactionDate);
        var transactionId = await GenerateNextTransactionId(date);

        return new Transaction(
            transactionId,
            transactionType: EnumHelpers.ConvertToEnum<TransactionType>(transactionType[0]),
            amount: AmountHelpers.ConvertToAmount(amount),
            transactionDate: date);
    }
}
