using AutoMapper;
using AwesomeGICBank.ApplicationServices.Features.AccountStatement.Queries.Monthly;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.ApplicationServices.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankStatementRecord, MonthlyAccountStatementRecord>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id.Value));
        CreateMap<BankStatement, MonthlyAccountStatementResponse>()
            .ForMember(dest => dest.MonthlyAccountStatementRecords,
               opt => opt.MapFrom(src => src.BankStatementRecords));

    }
}