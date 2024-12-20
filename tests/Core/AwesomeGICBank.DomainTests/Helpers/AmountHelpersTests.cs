using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;

namespace AwesomeGICBank.DomainTests.Helpers;

public class AmountHelpersTests
{
    [Theory]
    [InlineData("12")]
    [InlineData("12.0")]
    [InlineData("12.00")]
    [InlineData("12.1")]
    [InlineData("12.11")]
    public void ConvertToAmount_ShouldConvertToAmount_WhenPassStringWithCorrectForamt(string amount)
    {
        // Arrange
        decimal expectedAmount = Decimal.Parse(amount);

        // Act
        var actualAmount = AmountHelpers.ConvertToAmount(amount);

        // Assert
        Assert.Equal(expectedAmount, actualAmount);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ConvertToAmount_ShouldThrowArgumentNullException_WhenAmountIsNullOrEmpty(string amount)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => AmountHelpers.ConvertToAmount(amount));
    }

    [Theory]
    [InlineData("123.era")]
    [InlineData("234-45")]
    public void ConvertToAmount_ShouldThrowInvalidAmountException_WhenAmountIsNotAValidNumber(string amount)
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidAmountException>(() => AmountHelpers.ConvertToAmount(amount));
        Assert.Contains($"Invalid amount format: {amount}", exception.Message);
    }

    [Theory]
    [InlineData("12.111")]
    [InlineData("12.000")]
    [InlineData("12.1242")]
    public void ConvertToAmount_ShouldThrowInvalidAmountException_WhenAmountHasMoreThan2DecimalPoints(string amount)
    {
        // Arrange
        decimal expectedAmount = Decimal.Parse(amount);

        // Act & Assert
        var exception = Assert.Throws<InvalidAmountException>(() => AmountHelpers.ConvertToAmount(amount));
        Assert.Contains($"Amount cannot have more than 2 decimal places : {amount}", exception.Message);
    }
}
