using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.ValueObjects;

public class InterestRule
{
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public int SequenceNumber { get; set; }
    public decimal Rate { get; set; }

    public InterestRule(DateOnly date, int sequenceNumber, string name, decimal rate)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (rate <= 0 || rate >= 100)
            throw new InvalidRateException(rate);

        Date = date;
        SequenceNumber = sequenceNumber;
        Name = name;
        Rate = rate;
    }
}
