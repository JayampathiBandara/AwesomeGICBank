using AwesomeGICBank.ApplicationServices.Common.DTOs;

namespace AwesomeGICBank.ApplicationServices.Common.Parsers;

public static class TransactionDtoParser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">Input Format : 20230626 AC001 W 100.00</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static TransactionDto Parse(string? input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input));

        var transactionDetails = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (transactionDetails.Length != 4)
        {
            throw new ArgumentException("Invalid input format. Expected: <Date> <Account> <Type> <Amount>");
        }

        return new TransactionDto
        {
            TransactionDate = transactionDetails[0],
            AccountNo = transactionDetails[1],
            TransactionType = transactionDetails[2],
            Amount = transactionDetails[3]
        };
    }
}