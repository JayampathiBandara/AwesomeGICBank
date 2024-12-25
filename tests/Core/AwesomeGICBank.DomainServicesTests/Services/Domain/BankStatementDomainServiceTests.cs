using AutoMapper;
using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.MappingProfiles;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using AwesomeGICBank.DomainServices.Services.Persistence;
using Moq;
using Shouldly;


namespace AwesomeGICBank.DomainServicesTests.Services.Domain;


public class BankStatementDomainServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<ITransactionRepository> _transactionRepository;
    private readonly Mock<IInterestCalculatorDomainService> _interestCalculatorDomainService;
    private readonly IBankStatementDomainService _bankStatementDomainService;


    /*
     Account: AC001
    | Date     | Txn Id      | Type | Amount | Balance |
    | 20230601 | 20230601-01 | D    | 150.00 |  250.00 |
    | 20230626 | 20230626-01 | W    |  20.00 |  230.00 |
    | 20230626 | 20230626-02 | W    | 100.00 |  130.00 |
    | 20230630 |             | I    |   0.39 |  130.39 |
    */
    public BankStatementDomainServiceTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // Create mapper instance
        _mapper = configuration.CreateMapper();

        _transactionRepository = new Mock<ITransactionRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _interestCalculatorDomainService = new Mock<IInterestCalculatorDomainService>();

        _unitOfWork.Setup(uow => uow.TransactionRepository)
            .Returns(_transactionRepository.Object);


        _bankStatementDomainService = new BankStatementDomainService(_mapper,
            _unitOfWork.Object,
            _interestCalculatorDomainService.Object);
    }

    [Fact]
    public async Task GenerateStatement_ShouldGenerateBankStatement_WhenAccountNumberAndYearMonthProvided()
    {
        // Arrange
        string accountNo = "Acc01";
        List<Transaction> transactions = new()
        {
            new (new TransactionId(new DateOnly(2023,06,1),1), TransactionType.Deposit, 150m, new DateOnly(2023, 06, 1))
            {
                AccountNo = accountNo
            },
            new (new TransactionId(new DateOnly(2023,06,26),1), TransactionType.Withdrawal, 20m,new DateOnly(2023,06,26))
            {
                AccountNo = accountNo
            },
            new (new TransactionId(new DateOnly(2023,06,26),2), TransactionType.Withdrawal, 100m, new DateOnly(2023,06,26))
            {
                AccountNo = accountNo
            }
        };

        _transactionRepository
           .Setup(repo => repo.GetBalanceAsOfDateAsync(accountNo, new DateOnly(2023, 06, 1)))
           .ReturnsAsync(100m);

        _transactionRepository
            .Setup(repo => repo.GetAsync(It.IsAny<string>(), new DateOnly(2023, 06, 1), new DateOnly(2023, 06, 30)))
            .ReturnsAsync(transactions);

        _interestCalculatorDomainService
            .Setup(repo => repo.CalculateMonthlyInterestAsync("Acc01", 2023, 6))
            .ReturnsAsync(0.39m);

        var bankStatement = await _bankStatementDomainService.GenerateStatementAsync("Acc01", 2023, 6);

        bankStatement.AccountNo.ShouldBe(accountNo);
        bankStatement.BankStatementRecords.Count.ShouldBe(4);

        var record = bankStatement.BankStatementRecords[0];
        record.Id.Value.ShouldBe("20230601-01");
        record.Amount.ShouldBe(150);
        record.Type.ShouldBe(TransactionType.Deposit);
        record.RunningBalance.ShouldBe(250);

        record = bankStatement.BankStatementRecords[1];
        record.Id.Value.ShouldBe("20230626-01");
        record.Amount.ShouldBe(20);
        record.Type.ShouldBe(TransactionType.Withdrawal);
        record.RunningBalance.ShouldBe(230);

        record = bankStatement.BankStatementRecords[2];
        record.Id.Value.ShouldBe("20230626-02");
        record.Amount.ShouldBe(100);
        record.Type.ShouldBe(TransactionType.Withdrawal);
        record.RunningBalance.ShouldBe(130);

        var interstRecord = bankStatement.BankStatementRecords[3];
        interstRecord.Id.ShouldBeNull();
        interstRecord.Date.ShouldBe(new DateOnly(2023, 06, 30));
        interstRecord.Amount.ShouldBe(0.39m);
        interstRecord.Type.ShouldBe(TransactionType.Interest);
        interstRecord.RunningBalance.ShouldBe(130.39m);

    }
}
