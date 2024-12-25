using AwesomeGICBank.DomainServices.Services.Persistence;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.SqlServerPersistence;

public static class SqlServerPersistenceServiceRegistration
{
    public static IServiceCollection AddSqlServerPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AwesomeGICBankDbContext>(options =>
        {
            var connectionString = configuration["ConnectionStrings:GicBankConnection"];
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                MultipleActiveResultSets = true
            };

            options.UseSqlServer(builder.ConnectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
