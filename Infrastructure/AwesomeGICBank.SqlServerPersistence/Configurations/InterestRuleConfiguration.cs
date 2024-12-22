using AwesomeGICBank.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeGICBank.SqlServerPersistence.Configurations;

public class InterestRuleConfiguration : IEntityTypeConfiguration<InterestRule>
{
    public void Configure(EntityTypeBuilder<InterestRule> builder)
    {
        builder.ToTable("InterestRules", "rules");

        builder.HasKey(t => t.Date);

        builder.Property(e => e.RuleId)
        .IsRequired()
        .HasMaxLength(20);

        builder.Property(e => e.Date)
        .IsRequired();

        builder.Property(e => e.Rate)
        .IsRequired()
        .HasColumnType("decimal(18,2)");
    }
}
