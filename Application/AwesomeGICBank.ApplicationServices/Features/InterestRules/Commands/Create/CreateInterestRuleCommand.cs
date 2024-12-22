using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;

public class CreateInterestRuleCommand : IRequest<BaseResponse<InterestRulesResponse>>
{
    public InterestRuleDto InterestRule { get; set; }
}
