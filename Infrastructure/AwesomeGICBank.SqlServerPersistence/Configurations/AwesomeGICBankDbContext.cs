using AwesomeGICBank.Domain.Entities;
using AwesomeGICBank.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGICBank.SqlServerPersistence.Configurations;

public class AwesomeGICBankDbContext : DbContext
{
    public AwesomeGICBankDbContext(DbContextOptions<AwesomeGICBankDbContext> options)
    : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<InterestRule> InterestRules { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new InterestRuleConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

    }
}
