using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using Moq;

namespace AwesomeGICBank.DomainServicesTests;

public class TransactionDomainServiceTests
{

    private readonly Mock<ITransactionRepository> _transactionRepository;
    private readonly TransactionDomainService _transactionDomainService;

    public TransactionDomainServiceTests()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
        _transactionDomainService = new TransactionDomainService(_transactionRepository.Object);
    }

    [Fact]
    public void GenerateNextTransactionId_ShouldReturnOne_WhenNoRecordExistsForTheDay()
    {
        // Arrange
        var testDate = new DateOnly(2024, 12, 20);
        _transactionRepository
            .Setup(x => x.GetMaximumTransactionNoAsync(testDate))
            .Returns((string)null);

        // Act
        var result = _transactionDomainService.GenerateNextTransactionId(testDate);

        // Assert
        Assert.Equal("20241220_01", result.Value);
    }

    [Theory]
    [InlineData("20241220-01", "20241220_02")]
    [InlineData("20241220-09", "20241220_10")]
    [InlineData("20241220-99", "20241220_100")]
    public void GenerateNextTransactionId_ShouldReturnNextNumber_WhenLatestSequenceNumberIsReturnForTheDay(
        string latestTransactioId,
        string newTransactioId)
    {
        // Arrange
        var transactionDate = new DateOnly(2024, 12, 20);
        _transactionRepository
            .Setup(x => x.GetMaximumTransactionNoAsync(transactionDate))
            .Returns(latestTransactioId);

        // Act
        var result = _transactionDomainService.GenerateNextTransactionId(transactionDate);

        // Assert
        Assert.Equal(newTransactioId, result.Value);
    }
}