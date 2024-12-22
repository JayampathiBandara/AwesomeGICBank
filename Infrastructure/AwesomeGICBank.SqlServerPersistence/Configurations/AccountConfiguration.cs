using AwesomeGICBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AwesomeGICBank.SqlServerPersistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "acc");
        builder.HasKey(e => e.AccountNo);

        builder.Property(e => e.AccountNo)
        .IsRequired()
        .HasMaxLength(10);
    }
}
