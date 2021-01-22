using GS.Document.Domain.Aggregates;
using GS.Document.Domain.Framework.Repositories;
using GS.Document.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Document.Infra.Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DocumentContext context;

        public CustomerRepository(DocumentContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return context;
            }
        }

        public async Task<CustomerAggregate> FindOrCreateAsync(Guid customerId)
        {
            var query = context.Customers.Where(x => x.Id == customerId);
            var document = await query.SingleOrDefaultAsync();

            if (document == null)
            {
                document = CustomerAggregate.CreateFrom(customerId);
                context.Add(document);
                context.SaveChanges();
            }

            //Load related data
            await query
               .Include(x => x.Documents)
               .SelectMany(x => x.Documents)
               .LoadAsync();

            return document;
        }
    }
}
