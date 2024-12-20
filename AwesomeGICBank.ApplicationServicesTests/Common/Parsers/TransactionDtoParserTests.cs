using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Common.Parsers;

namespace AwesomeGICBank.ApplicationServicesTests.Common.Parsers;

public class TransactionDtoParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Parse_ShouldThrowArgumentNullException_WhenInputIsNullOrEmpty(string input)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TransactionDtoParser.Parse(input));
    }

    [Theory]
    [InlineData("20230626 W 100.00")]
    [InlineData("20230626 AC001")]
    public void Parse_ShouldThrowFormatException_WhenInputIsInIvalidFormat(string input)
    {
        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => TransactionDtoParser.Parse(input));
        Assert.Contains($"Invalid input format. Expected: <Date> <Account> <Type> <Amount>", exception.Message);
    }

    [Fact]
    public void Parse_ShouldReturnTransactionDto_WhenInputIsInCorrectFormat()
    {
        string input = "20230626 AC001 W 100.00";
        var expectedDto = new TransactionDto
        {
            TransactionDate = "20230626",
            AccountNo = "AC001",
            TransactionType = "W",
            Amount = "100.00"
        };

        var transactioDto = TransactionDtoParser.Parse(input);
        Assert.Equal(
            new { transactioDto.TransactionDate, transactioDto.AccountNo, transactioDto.TransactionType, transactioDto.Amount },
            new { expectedDto.TransactionDate, expectedDto.AccountNo, expectedDto.TransactionType, expectedDto.Amount });

    }
}