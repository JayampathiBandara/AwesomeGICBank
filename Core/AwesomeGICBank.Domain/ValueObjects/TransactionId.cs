namespace AwesomeGICBank.Domain.ValueObjects;

public class TransactionId
{
    public string Value { get; }

    protected TransactionId() { }

    public TransactionId(DateOnly date, uint sequence)
    {
        Value = $"{date:yyyyMMdd}-{sequence:D2}";
    }
}