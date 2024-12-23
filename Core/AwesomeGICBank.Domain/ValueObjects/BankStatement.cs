using AwesomeGICBank.Domain.DataTypes;

namespace AwesomeGICBank.Domain.ValueObjects;

public record BankStatement
{
    public string AccountNo { get; set; }
    public List<BankStatementRecord> BankStatementRecords { get; set; }
}

public record BankStatementRecord
{
    public TransactionId Id { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public decimal RunningBalance { get; set; }
}
