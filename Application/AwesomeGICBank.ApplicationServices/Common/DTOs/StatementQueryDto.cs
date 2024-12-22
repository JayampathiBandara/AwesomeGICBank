namespace AwesomeGICBank.ApplicationServices.Common.DTOs;

public record StatementQueryDto
{
    public string YearMonth { get; init; }
    public string AccountNo { get; init; }

    public int Year => int.Parse(YearMonth.Substring(0, 4));
    public int Month => int.Parse(YearMonth.Substring(4, 2));
}
