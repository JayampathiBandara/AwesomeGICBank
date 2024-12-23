using AutoMapper;
using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using AwesomeGICBank.DomainServices.Services.Persistence;

namespace AwesomeGICBank.DomainServices.Services.Domain;

public class BankStatementDomainService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IInterestCalculatorDomainService interestCalculatorDomainService)
    : IBankStatementDomainService
{
    public async Task<BankStatement> GenerateStatementAsync(
        string accountNo,
        int year,
        int month)
    {
        var (startDate, endDate) = DateTimeHelpers.GetStartDateAndEndDate(year, month);

        var balanceAsAtStartDate = await unitOfWork
            .TransactionRepository
            .GetBalanceAsOfDateAsync(accountNo, startDate);

        var transactions = await unitOfWork
            .TransactionRepository
            .GetAsync(accountNo, startDate, endDate);

        var (bankStatementRecords, runningBalance) = GenerateStatementRecords(transactions, balanceAsAtStartDate);

        var interest = await interestCalculatorDomainService
            .CalculateMonthlyInterestAsync(accountNo, year, month);

        var bankStatement = new BankStatement()
        {
            AccountNo = accountNo,
            BankStatementRecords = bankStatementRecords.ToList()
        };

        bankStatement
            .BankStatementRecords
            .Add(new BankStatementRecord()
            {
                Amount = interest,
                Date = endDate,
                Type = TransactionType.Interest,
                RunningBalance = interest + runningBalance
            });

        return bankStatement;
    }

    private (IEnumerable<BankStatementRecord>, decimal) GenerateStatementRecords(
        IEnumerable<Transaction> transactions,
        decimal previousBalance)
    {
        List<BankStatementRecord> bankStatementRecords = new();
        decimal runningBalance = previousBalance;

        foreach (var transaction in transactions
            .OrderBy(t => t.Date)
            .ThenBy(t => t.TransactionId))
        {
            runningBalance += transaction.SignedAmount;

            var bankStatementRecord = mapper.Map<BankStatementRecord>(transaction);
            bankStatementRecord.RunningBalance = runningBalance;
            bankStatementRecords.Add(bankStatementRecord);
        }
        return (bankStatementRecords, runningBalance);
    }
}
