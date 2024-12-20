using AwesomeGICBank.DomainServices.Services.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.DomainServices;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionDomainService, TransactionDomainService>();

        return services;
    }
}
