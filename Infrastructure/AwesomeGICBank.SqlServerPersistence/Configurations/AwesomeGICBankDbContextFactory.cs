using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AwesomeGICBank.SqlServerPersistence.Configurations;

public class AwesomeGICBankDbContextFactory : IDesignTimeDbContextFactory<AwesomeGICBankDbContext>
{
    public AwesomeGICBankDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AwesomeGICBankDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("GicBankConnection"));

        return new AwesomeGICBankDbContext(optionsBuilder.Options);
    }
}