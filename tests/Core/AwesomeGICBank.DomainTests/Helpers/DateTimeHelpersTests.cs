using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;

namespace AwesomeGICBank.DomainTests.Helpers;


public class DateTimeHelpersTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ConvertDateStringToDateOnly_ShouldThrowArgumentNullException_WhenDateIsNullOrEmpty(string date)
    {
        Assert.Throws<ArgumentNullException>(() => DateTimeHelpers.ConvertDateStringToDateOnly(date));
    }

    [Theory]
    [InlineData("20241231")]
    [InlineData("20241201")]
    public void ConvertDateStringToDateOnly_ShouldConvertToDateOnlyType_WhenPassStringWithCorrectForamt(string date)
    {
        // Arrange
        DateOnly expectedDate = new DateOnly(2024, 12, int.Parse(date.Substring(6, 2)));

        // Act
        DateOnly actualDate = DateTimeHelpers.ConvertDateStringToDateOnly(date);

        // Assert
        Assert.Equal(expectedDate, actualDate);
    }

    [Theory]
    [InlineData("2024-12-31")]
    [InlineData("20122024")]
    [InlineData("20190229")]
    public void ConvertDateStringToDateOnly_ShouldThrowUnsupportedDateFormatException_WhenPassStringWithCorrectForamt(string date)
    {
        // Act & Assert
        var exception = Assert.Throws<UnsupportedDateFormatException>(() => DateTimeHelpers.ConvertDateStringToDateOnly(date));
        Assert.Contains($"Format of  date string {date} is not supported. supported format is yyyyMMdd", exception.Message);
    }
}
