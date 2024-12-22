using AwesomeGICBank.ApplicationServices;
using AwesomeGICBank.DomainServices;
using AwesomeGICBank.SqlServerPersistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeGICBank.ConsoleClient;

public class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var bankClient = new BankClient(serviceProvider);

        await bankClient.StartBankAsync();
    }

    private static IServiceProvider ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        IServiceCollection services = new ServiceCollection();

        services.AddSqlServerPersistenceServices(configuration);
        services.AddDomainServices();
        services.AddApplicationServices();

        return services.BuildServiceProvider();
    }

}
