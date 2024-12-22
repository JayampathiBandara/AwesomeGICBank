using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Queries;

public class GetInterestRulesQueryHandler :
    IRequestHandler<GetInterestRulesQuery, BaseResponse<InterestRulesResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInterestRulesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ??
            throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<InterestRulesResponse>> Handle(GetInterestRulesQuery request, CancellationToken cancellationToken)
    {
        var rules = await _unitOfWork.InterestRuleRepository.GetAllAsync();

        var interestRulesResponse = new InterestRulesResponse();
        foreach (var rule in rules)
        {
            interestRulesResponse
                .InterestRuleRecords
                .Add(new InterestRuleRecord()
                {
                    Date = rule.Date,
                    Rate = rule.Rate,
                    RuleId = rule.RuleId
                });
        }
        return new BaseResponse<InterestRulesResponse>(interestRulesResponse);
    }
}