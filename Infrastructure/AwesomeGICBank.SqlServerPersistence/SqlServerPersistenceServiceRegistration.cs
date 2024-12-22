using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.SqlServerPersistence;

public static class SqlServerPersistenceServiceRegistration
{
    public static IServiceCollection AddSqlServerPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AwesomeGICBankDbContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionStrings:GicBankConnection"]);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
