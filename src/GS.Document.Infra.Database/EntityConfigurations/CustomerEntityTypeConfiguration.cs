using GS.Document.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GS.Document.Infra.Database.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<CustomerAggregate>
    {
        public void Configure(EntityTypeBuilder<CustomerAggregate> builder)
        {
            builder.ToTable("customers", DocumentContext.DEFAULT_SCHEMA);

            builder
                .HasKey(x => x.Id)
                .HasName("pk_wallet_id");

            builder.Ignore(x => x.DomainEvents);

            builder
                .Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .HasMany(x => x.Documents)
                .WithOne()
                .HasForeignKey("_customerId")
                .HasConstraintName("fk_customer_documents")
                .OnDelete(DeleteBehavior.Restrict);

            var navigation = builder.Metadata.FindNavigation(nameof(CustomerAggregate.Documents));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
