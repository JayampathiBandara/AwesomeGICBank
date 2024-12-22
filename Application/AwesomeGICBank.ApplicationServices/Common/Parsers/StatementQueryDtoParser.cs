using AwesomeGICBank.ApplicationServices.Common.DTOs;

namespace AwesomeGICBank.ApplicationServices.Common.Parsers;

public static class StatementQueryDtoParser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">Input format <Account> <Year><Month>. Sample input : AC001 202306</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static StatementQueryDto Parse(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input));

        var interestRuleDetails = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (interestRuleDetails.Length != 2)
        {
            throw new FormatException("Invalid rule format. Expected: <Account> <Year><Month>");
        }

        return new StatementQueryDto
        {
            AccountNo = interestRuleDetails[0],
            YearMonth = interestRuleDetails[1]
        };
    }
}
