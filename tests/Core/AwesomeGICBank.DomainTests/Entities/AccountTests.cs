using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.Exceptions;
using AwesomeGICBank.Domain.Helpers;
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
        account.DoTransaction(new Transaction("20230505-01", TransactionType.Deposit, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
        account.DoTransaction(new Transaction("20230506-01", TransactionType.Withdrawal, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230506")));
        account.DoTransaction(new Transaction("20230507-01", TransactionType.Deposit, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230507")));
        account.DoTransaction(new Transaction("20230507-02", TransactionType.Withdrawal, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230507")));
        account.DoTransaction(new Transaction("20230507-03", TransactionType.Deposit, 125.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230507")));

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

        // Act
        account.DoTransaction(new Transaction("20230505-01", TransactionType.Deposit, 125.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));


        // Assert
        Assert.Equal(125.5M, account.Balance);
    }

    [Fact]
    public void DoTransaction_ShouldPerormWithdrawal_WhenTransactionTypeIsWithdrawal()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);
        account.DoTransaction(new Transaction("20230505-01", TransactionType.Deposit, 125.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));

        // Act
        account.DoTransaction(new Transaction("20230506-01", TransactionType.Withdrawal, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230506")));

        // Assert
        Assert.Equal(100, account.Balance);
    }


    [Fact]
    public void DoTransaction_ShouldThrowInsufficientBalanceException_WhenWidthrawalAmmountIsGreaterThanAccountBalance()
    {
        // Arrange
        string accountNo = "AC001";
        var account = new Account(accountNo);
        account.DoTransaction(new Transaction("20230505-01", TransactionType.Deposit, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505")));
        var transaction = new Transaction("20230506-01", TransactionType.Withdrawal, 125.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230506"));

        // Act & Assert
        Assert.Throws<InsufficientBalanceException>(() => account.DoTransaction(transaction));
    }


    [Fact]
    public void DoTransaction_ShouldThrowUnsupportedTransactionTypeException_WhenTryToDoUnsupportedTransaction()
    {

        // Arrange
        string accountNo = "AC001";
        var transaction = new Transaction("20230505-01", (TransactionType)1111, 25.5M, DateTimeHelpers.ConvertDateStringToDateOnly("20230505"));
        var account = new Account(accountNo);


        // Act & Assert
        Assert.Throws<UnsupportedTransactionTypeException>(() => account.DoTransaction(transaction));
    }
    #endregion
}