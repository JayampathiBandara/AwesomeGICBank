using AwesomeGICBank.ApplicationServices.Common.DTOs;

namespace AwesomeGICBank.ApplicationServices.Common.Parsers;

public static class InterestRuleDtoParser
{
    /// 
    /// 20230615 RULE03 2.20
    public static InterestRuleDto Parse(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input));
        var interestRuleDetails = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (interestRuleDetails.Length != 3)
        {
            throw new FormatException("Invalid rule format. Expected: <Date> <Rule> <Rate>");
        }

        return new InterestRuleDto
        {
            Date = interestRuleDetails[0],
            Name = interestRuleDetails[1],
            Rate = interestRuleDetails[2],
        };
    }
}
