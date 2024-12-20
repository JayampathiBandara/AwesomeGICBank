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
        var theDate = new DateOnly(2024, 12, 21);
        var transactionId = new TransactionId(theDate, 2);

        // Act
        var transaction = new Transaction(transactionId, TransactionType.Deposit, 100M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505"));

        // Assert
        Assert.Equal(transactionId, transaction.Id);
        Assert.Equal(100, transaction.Amount);
    }

    [Fact]
    public void Transaction_ShouldThrowArgumentNullException_WhenTransactionNumberIsNull()
    {
        //Arange 
        TransactionId transactionId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Transaction(transactionId, TransactionType.Deposit, 100M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Transaction_ShouldThrowInvalidAmountException_WhenTransactionAmountIsZeroOrMinus(decimal amount)
    {
        // Arrange
        var theDate = new DateOnly(2024, 12, 21);
        var transactionId = new TransactionId(theDate, 2);

        Assert.Throws<InvalidAmountException>(() => new Transaction(transactionId, TransactionType.Deposit, amount, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
    }
    #endregion
}
