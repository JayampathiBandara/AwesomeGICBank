using AwesomeGICBank.Domain.Exceptions;
using System.Globalization;

namespace AwesomeGICBank.Domain.Helpers;

public static class AmountHelpers
{
    public static decimal ConvertToAmount(string amountString)
    {
        if (string.IsNullOrEmpty(amountString))
            throw new ArgumentNullException(nameof(amountString));

        if (!decimal.TryParse(
            amountString,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out decimal amount))
        {
            throw new InvalidAmountException($"Invalid amount format: {amountString}");
        }

        var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
        if (decimalPlaces > 2)
        {
            throw new InvalidAmountException($"Amount cannot have more than 2 decimal places : {amountString}");
        }

        return amount;
    }
}
