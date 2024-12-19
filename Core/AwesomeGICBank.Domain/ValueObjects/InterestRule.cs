using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;

public class InterestRule
{
    public string Name { get; set; }
    public DateOnly CreatedDate { get; set; }
    public decimal Rate { get; set; }

    public InterestRule(string name, DateOnly createdDate, decimal rate)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (rate <= 0)
            throw new InvalidRateException(rate);

        Name = name;
        CreatedDate = createdDate;
        Rate = rate;
    }
}
