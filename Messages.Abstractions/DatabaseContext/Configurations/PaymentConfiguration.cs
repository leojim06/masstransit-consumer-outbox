using Messages.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messages.Abstractions.DatabaseContext.Configurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id);

        builder.Property(o => o.Amount)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
