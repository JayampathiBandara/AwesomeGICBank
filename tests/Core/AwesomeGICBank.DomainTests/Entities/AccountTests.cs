using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainTests.Entities;
public class AccountTests
{
    #region Account Creation
    [Fact]
    public void Account_ShouldCreateAnAccount_WhenAccountNumberIsValid()
    {
        // Arrange
        string accountNo = "AC001";

        // Act
        var account = new Account(accountNo);

        // Assert
        Assert.Equal(accountNo, account.AccountNo);
        Assert.Equal(0, account.Balance);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Account_ShouldThrowArgumentNullException_WhenAccountNumberIsNullOrEmpty(string accountNo)
    {
        Assert.Throws<ArgumentNullException>(() => new Account(accountNo));
    }
    #endregion

    #region Property : Balance
    [Fact]
    public void Balance_ShouldReturn_WhenTransactionsAreAvailable()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);

        // Act
        var theDate = new DateOnly(2024, 12, 21);
        account.DoTransaction(new Transaction(new TransactionId(theDate, 1), TransactionType.Deposit, 25.5M, theDate));
        account.DoTransaction(new Transaction(new TransactionId(theDate, 2), TransactionType.Withdrawal, 25.5M, theDate));
        var theDate1 = new DateOnly(2024, 12, 22);
        account.DoTransaction(new Transaction(new TransactionId(theDate1, 1), TransactionType.Deposit, 25.5M, theDate1));
        account.DoTransaction(new Transaction(new TransactionId(theDate1, 2), TransactionType.Withdrawal, 25.5M, theDate1));
        account.DoTransaction(new Transaction(new TransactionId(theDate1, 3), TransactionType.Deposit, 125.5M, theDate1));

        // Assert
        Assert.Equal(125.5M, account.Balance);
    }
    #endregion

    #region DoTransaction
    [Fact]
    public void DoTransaction_ShouldPerormDeposit_WhenTransactionTypeIsDeposit()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);
        var theDate = new DateOnly(2024, 12, 21);

        // Act
        account.DoTransaction(new Transaction(new TransactionId(theDate, 1), TransactionType.Deposit, 125.5M, theDate));

        // Assert
        Assert.Equal(125.5M, account.Balance);
    }

    [Fact]
    public void DoTransaction_ShouldPerormWithdrawal_WhenTransactionTypeIsWithdrawal()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);
        var theDate = new DateOnly(2024, 12, 21);
        account.DoTransaction(new Transaction(new TransactionId(theDate, 1), TransactionType.Deposit, 125.5M, theDate));

        // Act
        account.DoTransaction(new Transaction(new TransactionId(theDate, 2), TransactionType.Withdrawal, 25.5M, theDate));

        // Assert
        Assert.Equal(100, account.Balance);
    }


    [Fact]
    public void DoTransaction_ShouldThrowInsufficientBalanceException_WhenWidthrawalAmmountIsGreaterThanAccountBalance()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);
        var theDate = new DateOnly(2024, 12, 21);
        var transaction = new Transaction(new TransactionId(theDate, 2), TransactionType.Withdrawal, 125.5M, theDate);

        // Act & Assert
        Assert.Throws<InsufficientBalanceException>(() => account.DoTransaction(transaction));
    }


    [Fact]
    public void DoTransaction_ShouldThrowUnsupportedTransactionTypeException_WhenTryToDoUnsupportedTransaction()
    {

        // Arrange
        string accountNo = "AC001";
        var theDate = new DateOnly(2024, 12, 21);
        var transaction = new Transaction(new TransactionId(theDate, 2), (TransactionType)1111, 25.5M, theDate);
        var account = new Account(accountNo);


        // Act & Assert
        Assert.Throws<UnsupportedTransactionTypeException>(() => account.DoTransaction(transaction));
    }
    #endregion
}