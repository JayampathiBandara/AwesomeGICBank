using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.SqlServerPersistence;

public static class SqlServerPersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IInterestRuleRepository, InterestRuleRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
