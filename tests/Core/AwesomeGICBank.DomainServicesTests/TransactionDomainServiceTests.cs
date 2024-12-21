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
    public async Task GenerateNextTransactionId_ShouldReturnOne_WhenNoRecordExistsForTheDay()
    {
        // Arrange
        var testDate = new DateOnly(2024, 12, 20);
        _transactionRepository
           .Setup(x => x.GetMaximumTransactionIdAsync(testDate))
           .ReturnsAsync((string)null);

        // Act
        var result = await _transactionDomainService.GenerateNextTransactionId(testDate);

        // Assert
        Assert.Equal("20241220-01", result.Value);
    }

    [Theory]
    [InlineData("20241220-01", "20241220-02")]
    [InlineData("20241220-09", "20241220-10")]
    [InlineData("20241220-99", "20241220-100")]
    public async Task GenerateNextTransactionId_ShouldReturnNextNumber_WhenLatestSequenceNumberIsReturnForTheDay(
        string latestTransactioId,
        string newTransactioId)
    {
        // Arrange
        var transactionDate = new DateOnly(2024, 12, 20);
        _transactionRepository
            .Setup(x => x.GetMaximumTransactionIdAsync(transactionDate))
            .ReturnsAsync(latestTransactioId);

        // Act
        var result = await _transactionDomainService.GenerateNextTransactionId(transactionDate);

        // Assert
        Assert.Equal(newTransactioId, result.Value);
    }
}