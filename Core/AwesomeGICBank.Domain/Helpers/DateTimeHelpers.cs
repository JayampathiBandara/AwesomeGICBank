using AwesomeGICBank.Domain.Exceptions;
using System.Globalization;

namespace AwesomeGICBank.Domain.Helpers;

public static class DateTimeHelpers
{
    public static DateOnly ConvertDateStringToDateOnly(string date)
    {
        if (string.IsNullOrEmpty(date))
            throw new ArgumentNullException(nameof(date));

        string format = "yyyyMMdd";
        CultureInfo provider = CultureInfo.InvariantCulture;

        if (DateOnly.TryParseExact(date, format, provider, DateTimeStyles.None, out DateOnly result))
            return result;

        throw new UnsupportedDateFormatException($"Format of  date string {date} is not supported. supported format is {format}");
    }
}
