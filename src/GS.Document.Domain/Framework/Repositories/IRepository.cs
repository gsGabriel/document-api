using GS.Document.Domain.Framework.Aggregates;

namespace GS.Document.Domain.Framework.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
