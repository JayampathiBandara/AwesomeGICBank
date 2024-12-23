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

    public static (DateOnly StartDate, DateOnly EndDate) GetStartDateAndEndDate(int year, int month)
    {
        if (year < 1)
            throw new UnsupportedDateFormatException($"Invalid Year :{year}");

        if (month < 1 || month > 12)
            throw new UnsupportedDateFormatException($"Invalid Month :{month}");

        var startDate = new DateOnly(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return (startDate, endDate);
    }

    public static int GetDateDifference(DateOnly from, DateOnly to)
    {
        return to.DayNumber - from.DayNumber;
    }
}
