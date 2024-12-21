using AwesomeGICBank.Domain.Exceptions;
using System.Globalization;

namespace AwesomeGICBank.Domain.Helpers;

public static class NumericHelpers
{
    public static decimal ConvertToDecimal(string numberString)
    {
        if (string.IsNullOrEmpty(numberString))
            throw new ArgumentNullException(nameof(numberString));

        if (!decimal.TryParse(
            numberString,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out decimal amount))
        {
            throw new InvalidNumberException(numberString);
        }

        return amount;
    }
}
