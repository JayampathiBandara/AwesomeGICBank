using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;

public class InterestRule
{
    public string RuleId { get; set; }
    public DateOnly Date { get; set; }

    public decimal Rate { get; set; }

    protected InterestRule() { }

    public InterestRule(DateOnly date, string ruleId, decimal rate)
    {
        if (string.IsNullOrEmpty(ruleId))
            throw new ArgumentNullException(nameof(ruleId));

        if (rate <= 0 || rate >= 100)
            throw new InvalidRateException(rate);

        Date = date;
        RuleId = ruleId;
        Rate = rate;
    }
}
