using AwesomeGICBank.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeGICBank.SqlServerPersistence.Configurations;
public static class ValueObjectConversions
{
    public static PropertyBuilder<TransactionId> AsString(this PropertyBuilder<TransactionId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            value => CreateTransactionId(value));
    }

    private static TransactionId CreateTransactionId(string value)
    {
        var date = DateOnly.ParseExact(value.Substring(0, 8), "yyyyMMdd", null);
        var sequence = uint.Parse(value.Substring(9));
        return new TransactionId(date, sequence);
    }
}

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions", "acc");

        builder.HasKey(t => t.Id);
        builder.Property(e => e.Id)
            .AsString()
            .IsRequired()
            .HasMaxLength(15);


        builder.Property(e => e.Type)
        .IsRequired()
        .HasMaxLength(1);

        builder.Property(e => e.Date)
        .IsRequired();

        builder.Property(e => e.Amount)
        .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(e => e.Account)
        .WithMany(s => s.Transactions)
        .HasForeignKey(e => e.AccountNo)
        .IsRequired();
    }
}