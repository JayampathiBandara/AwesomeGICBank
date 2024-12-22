using AwesomeGICBank.ApplicationServices.Common.DTOs;
using AwesomeGICBank.ApplicationServices.Responses;
using AwesomeGICBank.Domain.Helpers;
using AwesomeGICBank.Domain.ValueObjects;
using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Persistence;
using MediatR;

namespace AwesomeGICBank.ApplicationServices.Features.InterestRules.Commands.Create;

public class CreateInterestRuleCommandCommandHandler
    : IRequestHandler<CreateInterestRuleCommand, BaseResponse>
{
    private readonly IInterestRuleDomainService _interestRuleDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInterestRuleCommandCommandHandler(
        IUnitOfWork unitOfWork,
        IInterestRuleDomainService interestRuleDomainService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        _interestRuleDomainService = interestRuleDomainService ?? throw new ArgumentNullException(nameof(interestRuleDomainService));
    }

    public async Task<BaseResponse> Handle(CreateInterestRuleCommand request, CancellationToken cancellationToken)
    {
        var validator = new InterestRuleDtoValidator();

        var validationResult = await validator.ValidateAsync(request.InterestRule);

        if (validationResult.Errors.Count > 0)
        {
            return new BaseResponse(validationResult.Errors);
        }

        var interestRuleDto = request.InterestRule;
        var date = DateTimeHelpers.ConvertDateStringToDateOnly(interestRuleDto.Date);

        var interestRuleSeqNo = await _interestRuleDomainService.GenerateNextRuleSequenceNoAsync(date);

        var interestRule = new InterestRule(
            date,
            interestRuleSeqNo,
            name: interestRuleDto.Name,
            rate: NumericHelpers.ConvertToDecimal(interestRuleDto.Rate));

        await _unitOfWork.InterestRuleRepository.CreateAsync(interestRule);

        return new BaseResponse();
    }
}
