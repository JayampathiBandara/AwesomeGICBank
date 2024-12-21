using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;

namespace AwesomeGICBank.DomainTests.Helpers;

public class NumericHelpersTests
{
    [Theory]
    [InlineData("12")]
    [InlineData("12.0")]
    [InlineData("12.00")]
    [InlineData("12.1")]
    [InlineData("12.112")]
    public void ConvertToDecimal_ShouldConvertToDecimal_WhenPassStringWithCorrectForamt(string number)
    {
        // Arrange
        decimal expectedNumber = Decimal.Parse(number);

        // Act
        var actualNumber = NumericHelpers.ConvertToDecimal(number);

        // Assert
        Assert.Equal(expectedNumber, actualNumber);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ConvertToDecimal_ShouldThrowArgumentNullException_WhenNumberIsNullOrEmpty(string number)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => NumericHelpers.ConvertToDecimal(number));
    }

    [Theory]
    [InlineData("123.era")]
    [InlineData("234-45")]
    public void ConvertToDecimal_ShouldThrowInvalidNumberException_WhenNumberIsNotAValidNumber(string number)
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidNumberException>(() => NumericHelpers.ConvertToDecimal(number));
        Assert.Contains($"{number} is an invalid decimal number", exception.Message);
    }
}
