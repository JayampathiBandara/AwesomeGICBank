using AwesomeGICBank.ConsoleClient.Enums;
using AwesomeGICBank.Domain.Helpers;

namespace AwesomeGICBank.ConsoleClient.Helpers;

public static class InputOperationReader
{
    public static Operation ReadOperation()
    {
        var key = Console.ReadKey();

        var operation = EnumHelpers.ConvertToEnum<Operation>(key.KeyChar);

        return operation;
    }
}
