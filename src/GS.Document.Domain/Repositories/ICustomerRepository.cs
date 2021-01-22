using GS.Document.Domain.Aggregates;
using GS.Document.Domain.Framework.Repositories;
using System;
using System.Threading.Tasks;

namespace GS.Document.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<CustomerAggregate>
    {
        Task<CustomerAggregate> FindOrCreateAsync(Guid customerId);
    }
}
