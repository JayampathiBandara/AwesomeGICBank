using AutoMapper;
using AwesomeGICBank.Domain.ValueObjects;

namespace AwesomeGICBank.DomainServices.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, BankStatementRecord>()
            .ForMember(dest => dest.RunningBalance, opt => opt.Ignore());

    }
}