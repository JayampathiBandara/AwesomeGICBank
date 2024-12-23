namespace AwesomeGICBank.DomainServices.Services.Domain.Interfaces;

public interface IInterestCalculatorDomainService
{
    Task<decimal> CalculateMonthlyInterestAsync(string accountNo, int year, int month);
}

