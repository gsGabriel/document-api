using GS.Document.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GS.Document.Infra.Database.EntityConfigurations
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Documents>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Documents> builder)
        {
            builder.ToTable("documents", DocumentContext.DEFAULT_SCHEMA);

            builder
                .HasKey("_id")
                .HasName("pk_wallet_id");

            builder.Ignore(x => x.Id);
            builder.Ignore(x => x.CustomerId);

            builder
                .Property<Guid>("_id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property<Guid>("_customerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("customer_id")
                .IsRequired();

            builder
                .Property(x => x.FileName)
                .HasColumnName("file_name")
                .IsRequired();

            builder
                .Property(x => x.ContentType)
                .HasColumnName("content_type")
                .IsRequired();

            builder
                .Property(x => x.Path)
                .HasColumnName("path")
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();
        }
    }
}
