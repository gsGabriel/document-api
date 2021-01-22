using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Application.Framework.CommandHandler
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        public Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
