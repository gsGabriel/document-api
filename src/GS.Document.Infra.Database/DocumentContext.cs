using GS.Document.Domain.Aggregates;
using GS.Document.Domain.Entities;
using GS.Document.Domain.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GS.Document.Infra.Database
{
    public class DocumentContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "public";

        public DocumentContext(DbContextOptions<DocumentContext> options)
            : base(options)
        { }

        public DbSet<CustomerAggregate> Customers { get; set; }
        public DbSet<Documents> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentContext).Assembly);
        }
    }
}
