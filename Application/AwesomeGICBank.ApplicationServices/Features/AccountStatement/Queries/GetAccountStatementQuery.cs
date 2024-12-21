using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.Domain.DataTypes;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries;

public class GeneralAccountStatementResponse
{
    public string AccountNo { get; set; }
    public List<GeneralAccountStatementRecord> AccountStatementRecordsq { get; private set; } = new();

    public class GeneralAccountStatementRecord
    {
        public DateOnly Date { get; init; }
        public string TransactionId { get; init; }
        public TransactionType Type { get; init; }
        public decimal Amount { get; init; }
    }
}
public class GetAccountStatementQuery : IRequest<BaseResponse<GeneralAccountStatementResponse>>
{
    public string AccountNo { get; set; }
}

public class GetAccountStatementQueryHandler : IRequestHandler<GetAccountStatementQuery, GeneralAccountStatementResponse>

{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionDomainService _transactionDomainService;

    public GetAccountStatementQueryHandler(
        IAccountRepository accountRepository,
        ITransactionDomainService transactionDomainService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _transactionDomainService = transactionDomainService ?? throw new ArgumentNullException(nameof(transactionDomainService));
    }

    public Task<GeneralAccountStatementResponse> Handle(GetAccountStatementQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    /*public async Task<GeneralAccountStatementResponse> Handle(GetAccountStatementQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAsync(request.AccountNo);

        var response = new GeneralAccountStatementResponse()
        {
            AccountNo = request.AccountNo,
            // AccountStatementRecordsq.AddRange()
        };
        return response;
    }*/
}