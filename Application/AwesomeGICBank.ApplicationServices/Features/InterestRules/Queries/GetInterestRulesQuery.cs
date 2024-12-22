using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Queries;

public class GetInterestRulesQuery : IRequest<BaseResponse<InterestRulesResponse>>
{
}
