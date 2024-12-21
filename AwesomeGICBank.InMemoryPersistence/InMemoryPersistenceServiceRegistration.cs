using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.InMemoryPersistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.InMemoryPersistence;

public static class InMemoryPersistenceServiceRegistration
{
    public static IServiceCollection AddInMemoryPersistenceServices(this IServiceCollection services)
    {
        // Register Bank as a singleton
        services.AddSingleton<Bank>();


        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IInterestRuleRepository, InterestRuleRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
