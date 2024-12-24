using AwesomeGICBank.ApplicationServices.Common.Enums;
using AwesomeGICBank.ApplicationServices.Common.Parsers;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;
using AwesomeGICBank.ApplicationServices.Features.InterestRules.Queries;
using AwesomeGICBank.ApplicationServices.Responses;
using MediatR;

namespace AwesomeGICBank.ConsoleClient.UserInterfaces;

public class InterestRuleInterface : BaseUserInterface
{
    public InterestRuleInterface(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Instruction = "\nPlease enter interest rules details in <Date> <RuleId> <Rate in %> format " +
            "\r\n(or enter blank to go back to main menu):" +
            "\r\n>";
    }

    protected override async Task<bool> ProcessInput(string input, IMediator mediator)
    {
        var interestRuleDto = InterestRuleDtoParser.Parse(input);
        var response = await mediator.Send(
            new CreateInterestRuleCommand()
            {
                InterestRule = interestRuleDto
            });
        if (response.ResponseType != ResponseTypes.Success)
        {
            Console.WriteLine(response.ToString());
            return true;
        }

        BaseResponse<InterestRulesResponse>
            result = await mediator.Send(new GetInterestRulesQuery() { });

        result.PrintResponse();

        return true;
    }
}
