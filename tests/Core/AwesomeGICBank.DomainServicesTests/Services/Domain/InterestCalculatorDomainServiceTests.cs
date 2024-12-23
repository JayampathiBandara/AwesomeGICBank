using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using Moq;
using Shouldly;

namespace AwesomeGICBank.DomainServicesTests.Services.Domain;

public class InterestCalculatorDomainServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ITransactionRepository> _transactionRepository;
    private readonly Mock<IInterestRuleRepository> _interestRuleRepository;
    private readonly InterestCalculatorDomainService _interestCalculatorDomainService;

    public InterestCalculatorDomainServiceTests()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
        _interestRuleRepository = new Mock<IInterestRuleRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _unitOfWork.Setup(uow => uow.TransactionRepository)
            .Returns(_transactionRepository.Object);
        _unitOfWork.Setup(uow => uow.InterestRuleRepository)
            .Returns(_interestRuleRepository.Object);

        _interestCalculatorDomainService = new InterestCalculatorDomainService(_unitOfWork.Object);
    }

    /*
    Account: AC001
    | Date     | Txn Id      | Type | Amount | Balance |
    | 20230601 | 20230601-01 | D    | 150.00 |  250.00 |
    | 20230626 | 20230626-01 | W    |  20.00 |  230.00 |
    | 20230626 | 20230626-02 | W    | 100.00 |  130.00 |
    | 20230630 |             | I    |   0.39 |  130.39 |

    Interest rules:
    | Date     | RuleId | Rate (%) |
    | 20230101 | RULE01 |     1.95 |
    | 20230520 | RULE02 |     1.90 |
    | 20230615 | RULE03 |     2.20 |
    */
    [Fact]
    public async Task GetInterestCalculationDate_ShouldReturnNextTRansaction_WhenNextTransactionRecordIsAvailable()
    {
        // Arrange
        List<Transaction> transactions = new()
        {
            new (new TransactionId(new DateOnly(2023,06,1),1), TransactionType.Deposit, 150m, new DateOnly(2023,06,1)),
            new (new TransactionId(new DateOnly(2023,06,26),1), TransactionType.Withdrawal, 20m,new DateOnly(2023,06,26)),
            new (new TransactionId(new DateOnly(2023,06,26),2), TransactionType.Withdrawal, 100m, new DateOnly(2023,06,26))
        };

        List<InterestRule> interestRules = new()
        {
            new (new DateOnly(2023, 1, 01), "RULE01", 1.95M),
            new (new DateOnly(2023, 5, 20), "RULE02", 1.90M),
            new (new DateOnly(2023, 6, 15), "RULE03", 2.20M)
        };

        _transactionRepository
            .Setup(repo => repo.GetBalanceAsOfDateAsync("Acc01", new DateOnly(2023, 06, 1)))
            .ReturnsAsync(100m);

        _transactionRepository
            .Setup(repo => repo.GetAsync(It.IsAny<string>(), new DateOnly(2023, 06, 1), new DateOnly(2023, 06, 30)))
            .ReturnsAsync(transactions);

        _interestRuleRepository
        .Setup(repo => repo.GetAllAsync())
        .ReturnsAsync(interestRules);

        // Act
        var currentInterestRule = await _interestCalculatorDomainService.CalculateMonthlyInterestAsync("Acc01", 2023, 6);

        // Assert
        currentInterestRule.ShouldBe(0.39m);
    }
}