namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

public record TransactionDto
{
    public string TransactionDate { get; init; }
    public string AccountNo { get; init; }
    public string TransactionType { get; init; }
    public string Amount { get; init; }
}
