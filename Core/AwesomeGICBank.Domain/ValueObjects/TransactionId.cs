﻿namespace AwesomeGICBank.Domain.ValueObjects;

public class TransactionId
{
    public string Value { get; }

    public TransactionId(DateOnly date, uint sequence)
    {
        Value = $"{date:yyyyMMdd}_{sequence:D2}";
    }
}