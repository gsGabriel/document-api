using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Infra.Kafka.Interfaces
{
    public interface IEventProducer : IDisposable
    {
        Task<bool> ProduceAsync<T>(string topic, IEnumerable<T> values, CancellationToken cancellationToken = default);
        Task<bool> ProduceAsync<T>(string topic, T value, CancellationToken cancellationToken = default);
    }
}
