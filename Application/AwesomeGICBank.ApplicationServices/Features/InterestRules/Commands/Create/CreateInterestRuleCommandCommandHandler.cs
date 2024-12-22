using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;

public class CreateInterestRuleCommandCommandHandler
    : IRequestHandler<CreateInterestRuleCommand, BaseResponse<InterestRulesResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInterestRuleCommandCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BaseResponse<InterestRulesResponse>> Handle(CreateInterestRuleCommand request, CancellationToken cancellationToken)
    {
        var validator = new InterestRuleDtoValidator();

        var validationResult = await validator.ValidateAsync(request.InterestRule);

        if (validationResult.Errors.Count > 0)
        {
            return new BaseResponse<InterestRulesResponse>(validationResult.Errors);
        }

        var interestRuleDto = request.InterestRule;
        var date = DateTimeHelpers.ConvertDateStringToDateOnly(interestRuleDto.Date);

        var interestRule = new InterestRule(
            date,
            ruleId: interestRuleDto.Name,
            rate: NumericHelpers.ConvertToDecimal(interestRuleDto.Rate));

        await _unitOfWork.InterestRuleRepository.CreateOrUpdateAsync(interestRule);
        await _unitOfWork.SaveAsync();
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
