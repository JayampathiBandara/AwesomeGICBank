﻿using System.Globalization;

namespace AwesomeGICBank.ApplicationServices.Common;

public static class ClientInputValidators
{
    public static bool IsDecimal(string numberString)
    {
        return
            decimal.TryParse(
            numberString,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out decimal amount);
    }

    public static bool IsDateOnly(string date)
    {
        string format = "yyyyMMdd";
        CultureInfo provider = CultureInfo.InvariantCulture;

        return DateOnly.TryParseExact(date, format, provider, DateTimeStyles.None, out _);

    }

    public static bool IsYearMonthOnly(string yearMonth)
    {
        string format = "yyyyMM";
        CultureInfo provider = CultureInfo.InvariantCulture;

        return DateOnly.TryParseExact(yearMonth, format, provider, DateTimeStyles.None, out _);
    }
}

