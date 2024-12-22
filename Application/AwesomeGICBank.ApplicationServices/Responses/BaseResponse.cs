using AwesomeGICBank.ApplicationServices.Common.Enums;
using FluentValidation.Results;

namespace AwesomeGICBank.ApplicationServices.Responses;

public class BaseResponse
{
    public ResponseTypes ResponseType { get; private set; }
    public List<string> Messages { get; private set; } = new();
    public BaseResponse(ResponseTypes responseType = ResponseTypes.Success)
    {
        ResponseType = responseType;
    }

    public BaseResponse(ResponseTypes responseType, string message)
    {
        ResponseType = responseType;
        Messages.Add(message);
    }

    public BaseResponse(List<ValidationFailure> validationResult)
    {
        SetValidationErrors(validationResult);
    }

    private void SetValidationErrors(List<ValidationFailure> validationResult)
    {
        if (validationResult == null || !validationResult.Any())
        {
            return;
        }

        ResponseType = ResponseTypes.ClientError;

        foreach (var error in validationResult)
        {
            Messages.Add(error.ErrorMessage);
        }
    }

    public override string ToString()
    {
        return $"\n{ResponseType}: \n {string.Join("\n ", Messages)}\n";

    }
}

public class BaseResponse<T> : BaseResponse where T : class?
{
    public T ReturnValue { get; private set; }
    public BaseResponse(T returnValue) : base()
    {
        ReturnValue = returnValue;
    }

    public BaseResponse(ResponseTypes responseType, string message) : base(responseType, message) { }

    public BaseResponse(List<ValidationFailure> validationResult) : base(validationResult) { }

    public void PrintResponse()
    {
        if (ResponseType == ResponseTypes.Success)
        {
            Console.WriteLine(ReturnValue.ToString());
        }
        else
        {
            Console.WriteLine(this.ToString());
        }
    }
}