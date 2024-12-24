using AwesomeGICBank.DomainServices.Services.Domain;
using AwesomeGICBank.DomainServices.Services.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.DomainServices;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IBankStatementDomainService, BankStatementDomainService>();
        services.AddScoped<ITransactionDomainService, TransactionDomainService>();
        services.AddScoped<IInterestCalculatorDomainService, InterestCalculatorDomainService>();

        return services;
    }
}
