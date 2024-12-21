using AwesomeGICBank.Domain.Exceptions;

namespace AwesomeGICBank.Domain.Helpers;

public static class AmountHelpers
{
    public static decimal ConvertToAmount(string amountString)
    {
        decimal amount = NumericHelpers.ConvertToDecimal(amountString);

        var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
        if (decimalPlaces > 2)
        {
            throw new InvalidAmountException($"Amount cannot have more than 2 decimal places : {amountString}");
        }

        return amount;
    }
}
