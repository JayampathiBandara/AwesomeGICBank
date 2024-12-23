using AwesomeGICBank.Domain.DataTypes;
using System.Text;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;

public class MonthlyAccountStatementResponse
{
    public string AccountNo { get; set; }
    public List<MonthlyAccountStatementRecord> MonthlyAccountStatementRecords { get; private set; }// = new();

    public override string ToString()
    {
        var statement = new StringBuilder();

        statement.AppendLine($"Account: {AccountNo}");
        statement.AppendLine();

        statement.AppendLine("| Date     | Txn Id      | Type | Amount  | Balance |");

        foreach (var record in MonthlyAccountStatementRecords)
        {
            statement.AppendLine(
                $"| {record.Date:yyyyMMdd} | {record.TransactionId,-11} | {(char)record.Type,-4} | {record.Amount,7:F2} | {record.RunningBalance,7:F2} |");
        }

        return statement.ToString();
    }
}
public class MonthlyAccountStatementRecord
{
    public DateOnly Date { get; init; }
    public string TransactionId { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public decimal RunningBalance { get; init; }
}



