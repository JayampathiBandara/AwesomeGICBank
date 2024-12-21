namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

public record InterestRuleDto
{
    public string Date { get; init; }
    public string Name { get; init; }
    public string Rate { get; init; }
}
