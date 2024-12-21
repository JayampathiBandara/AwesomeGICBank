using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainTests.ValueObjects;

public class InterestRuleTests
{
    #region InterestRule Creation
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void InterestRule_ShouldThrowArgumentNullException_WhenInterestRuleIdIsNullOrEmpty(string ruleId)
    {
        Assert.Throws<ArgumentNullException>(() => new InterestRule(DateTimeHelpers.ConvertDateStringToDateOnly("20230505"), 1, ruleId, 1.5M));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(101)]
    public void InterestRule_ShouldThrowInvalidRateException_WhenInterestRateValueIsZeroOrMinus(decimal rate)
    {
        Assert.Throws<InvalidRateException>(() => new InterestRule(DateTimeHelpers.ConvertDateStringToDateOnly("20230505"), 1, "RULE03", rate));
    }
    #endregion
}
