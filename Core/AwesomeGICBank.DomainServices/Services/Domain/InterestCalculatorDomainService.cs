using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class InterestCalculatorDomainService : IInterestCalculatorDomainService
{
    private readonly IUnitOfWork _unitOfWork;
    public InterestCalculatorDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public static IEnumerable<(Transaction Transaction, decimal RunningBalance)> CalculateRunningBalances(
    IEnumerable<Transaction> transactions)
    {
        decimal runningBalance = 0;
        foreach (var transaction in transactions.OrderBy(t => t.Date).ThenBy(t => t.TransactionId))
        {
            runningBalance += transaction.SignedAmount;
            yield return (transaction, runningBalance);
        }
    }

    public async Task<decimal> CalculateMonthlyInterestAsync(string accountNo, int year, int month)
    {
        var (monthStartDate, monthEndDate) = DateTimeHelpers.GetStartDateAndEndDate(year, month);

        var currentBalance = await _unitOfWork.TransactionRepository.GetBalanceAsOfDateAsync(accountNo, monthStartDate);
        var interest = 0m;
        var transactions = await _unitOfWork.TransactionRepository.GetAsync(accountNo, monthStartDate, monthEndDate);
        var interestRules = await _unitOfWork.InterestRuleRepository.GetAllAsync();

        var transactionsGroupByDate = transactions
            .GroupBy(t => t.Date)
            .Select(group => new
            {
                Date = group.Key,
                SignedAmount = group.Sum(t => t.SignedAmount)
            })
            .OrderBy(dt => dt.Date);

        foreach (var currentTransaction in transactionsGroupByDate)
        {
            currentBalance += currentTransaction.SignedAmount;
            var nextTransactionDate = GetNextTransactionDate(transactions, monthEndDate, currentTransaction.Date);
            var currentRule = GetCurrentInterestRule(interestRules, currentTransaction.Date);

            var interestCaluculationDate = nextTransactionDate ?? monthEndDate;

            var nextInterestRule = GetNextInterestRule(interestRules, currentRule.Date, interestCaluculationDate);

            if (nextTransactionDate is null && nextInterestRule is null)
            {
                // Neither transactions nor rule changes happen after this transaction until the end of the current month.
                var interestUntilMonthEnd = CalculateInterestAmount(
                    balance: currentBalance,
                    rate: currentRule.Rate,
                    from: currentTransaction.Date,
                    to: monthEndDate,
                    monthEndDate: monthEndDate);
                interest += interestUntilMonthEnd;
                break;
            }


            if (nextTransactionDate.HasValue && (nextInterestRule is null || nextTransactionDate <= nextInterestRule?.Date))
            {
                // Interest is calculated until the next transaction date. No rule changes occur within this period.
                var interestUntilNextTransaction = CalculateInterestAmount(
                    balance: currentBalance,
                    rate: currentRule.Rate,
                    from: currentTransaction.Date,
                    to: nextTransactionDate!.Value,
                    monthEndDate: monthEndDate);
                interest += interestUntilNextTransaction;
                break;
            }

            // A rule change occurs between the current transaction and the next transaction.
            // If there is no next transaction, the month-end date is used
            var interestBeforeRuleChange = CalculateInterestAmount(
                balance: currentBalance,
                rate: currentRule.Rate,
                from: currentTransaction.Date,
                to: nextInterestRule!.Date,
                monthEndDate: monthEndDate);

            var interestAfterRuleChange = CalculateInterestAmount(
                balance: currentBalance,
                rate: nextInterestRule.Rate,
                from: nextInterestRule.Date,
                to: interestCaluculationDate,
                monthEndDate: monthEndDate);

            interest = interestBeforeRuleChange + interestAfterRuleChange;
        }

        decimal roundedInterest = Math.Round(interest / 365, 2, MidpointRounding.AwayFromZero);
        return roundedInterest;
    }

    private static decimal CalculateInterestAmount(decimal balance, decimal rate, DateOnly from, DateOnly to, DateOnly monthEndDate)
    {
        var numberOfDates = DateTimeHelpers.GetDateDifference(from, to);
        if (monthEndDate == to)
        {
            numberOfDates++;
        }
        return balance * rate * numberOfDates / 100;
    }

    private static InterestRule? GetNextInterestRule(List<InterestRule> interestRules, DateOnly currentRuleDate, DateOnly interestCaluculationDate)
    {
        return interestRules
            .Where(x => x.Date > currentRuleDate && x.Date < interestCaluculationDate)
            .OrderBy(x => x.Date)
            .FirstOrDefault();
    }

    private static DateOnly? GetNextTransactionDate(List<Transaction> transactions, DateOnly monthEndDate, DateOnly currentTransactionDate)
    {
        return transactions
             .Where(x => x.Date > currentTransactionDate && x.Date < monthEndDate)
             .Select(x => (DateOnly?)x.Date)
             .Min();
    }

    private static InterestRule GetCurrentInterestRule(List<InterestRule> interestRules, DateOnly CurrentTransactionDate)
    {
        var interestRule = interestRules
            .Where(x => x.Date <= CurrentTransactionDate)
            .OrderByDescending(x => x.Date)
            .FirstOrDefault();

        if (interestRule == null)
        {
            throw new Exception($"Please define interes rule on or before {CurrentTransactionDate}");
        }
        return interestRule;
    }
}

