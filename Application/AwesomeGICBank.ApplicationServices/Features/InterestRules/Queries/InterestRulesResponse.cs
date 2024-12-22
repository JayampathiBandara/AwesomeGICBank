using System.Text;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Queries;

public class InterestRulesResponse
{
    public List<InterestRuleRecord> InterestRuleRecords { get; private set; } = new();

    public override string ToString()
    {
        var statement = new StringBuilder();

        statement.AppendLine("Interest rules:");
        statement.AppendLine();

        statement.AppendLine("| Date     | RuleId | Rate (%) |");

        foreach (var record in InterestRuleRecords)
        {
            statement.AppendLine(
                $"| {record.Date:yyyyMMdd} | {record.RuleId,-5} | {record.Rate,8:F2} |");
        }

        return statement.ToString();
    }
}
public class InterestRuleRecord
{
    public DateOnly Date { get; set; }
    public string RuleId { get; set; }
    public decimal Rate { get; set; }

}

