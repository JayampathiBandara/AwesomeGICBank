using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainTests.ValueObjects;

public class TransactionIdTests
{
    #region TransactionId Creation
    [Theory]
    [InlineData(2, "02")]
    [InlineData(20, "20")]
    [InlineData(200, "200")]
    [InlineData(9999, "9999")]
    public void TransactionId_ShouldCreateAnTransactionId_WhenTransactionNumberIsValid(uint SeqNo, string SeqNumber)
    {
        // Arrange
        var theDate = new DateOnly(2024, 12, 21);

        // Act
        var transactionId = new TransactionId(theDate, SeqNo);

        // Assert
        Assert.Equal($"20241221_{SeqNumber}", transactionId.Value);
    }
    #endregion
}
