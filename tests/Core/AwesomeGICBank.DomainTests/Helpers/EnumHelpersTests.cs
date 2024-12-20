using AwesomeGICBank.Domain.Helpers;

namespace AwesomeGICBank.DomainTests.Helpers;

public class EnumHelpersTests
{
    public enum TestEnum
    {
        Test = 'T',
        QA = 'Q'
    }

    [Theory]
    [InlineData('T', TestEnum.Test)]
    [InlineData('q', TestEnum.QA)]
    public void ConvertToEnum_ShouldConvertToEnumValue_WhenPassCorrectChar(char enumValue, TestEnum expected)
    {
        // Act
        var actualValue = EnumHelpers.ConvertToEnum<TestEnum>(enumValue);

        // Assert
        Assert.Equal(expected, actualValue);
    }

    [Fact]
    public void ConvertToEnum_ShouldThrowInvalidCastException_WhenTypeIsNotEnumType()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidCastException>(() => EnumHelpers.ConvertToEnum<int>('c'));
        Assert.Contains($"Invalid Enum type exception. Int32 is not an enum", exception.Message);
    }

    [Fact]
    public void ConvertToEnum_ShouldThrowInvalidCastException_WhenValueIsNotDefinedInsideEnum()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidCastException>(() => EnumHelpers.ConvertToEnum<TestEnum>('c'));
        Assert.Contains("Invalid value Exception. 'c' is not a valid enum value of TestEnum", exception.Message);
    }
}
