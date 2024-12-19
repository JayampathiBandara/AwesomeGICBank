using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainTests.ValueObjects;

public class TransactionTests
{
    #region Transaction Creation
    [Fact]
    public void Transaction_ShouldCreateAnTransaction_WhenTransactionNumberIsValid()
    {
        // Arrange
        string transactionId = "20230505-01";

        // Act
        var transaction = new Transaction(transactionId, TransactionType.Deposit, 100M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505"));

        // Assert
        Assert.Equal(transactionId, transaction.Id);
        Assert.Equal(100, transaction.Amount);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Transaction_ShouldThrowArgumentNullException_WhenTransactionNumberIsNullOrEmpty(string transactionId)
    {
        Assert.Throws<ArgumentNullException>(() => new Transaction(transactionId, TransactionType.Deposit, 100M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Transaction_ShouldThrowInvalidAmountException_WhenTransactionAmountIsZeroOrMinus(decimal amount)
    {
        // Arrange
        string transactionId = "20230505-01";

        Assert.Throws<InvalidAmountException>(() => new Transaction(transactionId, TransactionType.Deposit, amount, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
    }
    #endregion
}
