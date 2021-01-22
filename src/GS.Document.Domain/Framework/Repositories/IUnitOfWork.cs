using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Domain.Framework.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
