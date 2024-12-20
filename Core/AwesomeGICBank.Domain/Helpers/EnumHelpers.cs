namespace AwesomeGICBank.Domain.Helpers;

public static class EnumHelpers
{
    public static T ConvertToEnum<T>(char value) where T : struct
    {
        if (!typeof(T).IsEnum)
        {
            throw new InvalidCastException($"Invalid Enum type exception. {typeof(T).Name} is not an enum");
        }

        foreach (T operation in Enum.GetValues(typeof(T)))
        {
            if (Convert.ToChar(operation) == char.ToUpper(value))
            {
                return operation;
            }
        }

        throw new InvalidCastException($"Invalid value Exception. '{value}' is not a valid enum value of {typeof(T).Name}");
    }
}
