using AutoMapper;
using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;

public class GetMonthlyAccountStatementQueryHandler :
    IRequestHandler<GetMonthlyAccountStatementQuery, BaseResponse<MonthlyAccountStatementResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBankStatementDomainService _bankStatementDomainService;
    public GetMonthlyAccountStatementQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBankStatementDomainService bankStatementDomainService)
    {
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ??
            throw new ArgumentNullException(nameof(unitOfWork));
        _bankStatementDomainService = bankStatementDomainService ??
            throw new ArgumentNullException(nameof(bankStatementDomainService));
    }

    public async Task<BaseResponse<MonthlyAccountStatementResponse>> Handle(
        GetMonthlyAccountStatementQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new StatementQueryDtoValidator();

        var validationResult = await validator.ValidateAsync(request.StatementQuery);

        if (validationResult.Errors.Count > 0)
        {
            return new BaseResponse<MonthlyAccountStatementResponse>(validationResult.Errors);
        }

        if (!await _unitOfWork.AccountRepository.ExistsAsync(request.StatementQuery.AccountNo))
        {
            return new BaseResponse<MonthlyAccountStatementResponse>(ResponseTypes.ClientError, "Account not found");
        }

        var statement = await _bankStatementDomainService
            .GenerateStatementAsync(request.StatementQuery.AccountNo, request.StatementQuery.Year, request.StatementQuery.Month);

        var response = _mapper.Map<MonthlyAccountStatementResponse>(statement);

        return new BaseResponse<MonthlyAccountStatementResponse>(response);
    }
}