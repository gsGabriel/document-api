using Confluent.Kafka;
using GS.Document.Infra.Kafka.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Infra.Kafka
{
    public class EventProducer : IEventProducer
    {
        private readonly IProducer<string, string> producer;
        private readonly ILogger<EventProducer> logger;

        public EventProducer(ProducerConfig config, ILogger<EventProducer> logger)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            producer = new ProducerBuilder<string, string>(config).Build();
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ProduceAsync<T>(string topic, IEnumerable<T> values, CancellationToken cancellationToken = default)
        {
            var policy = Policy.Handle<Exception>()
                   .WaitAndRetryAsync(
                            retryCount: 3,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retry}. Re-trying", exception.GetType().Name, exception.Message, retry);
                            });

            await policy.ExecuteAsync(async token =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    foreach (var value in values)
                    {
                        await producer.ProduceAsync(topic, new Message<string, string> { Value = JsonConvert.SerializeObject(value) }, token);
                    }
                }
            }, cancellationToken);

            logger.LogInformation("Event published!");
            return true;
        }

        public async Task<bool> ProduceAsync<T>(string topic, T value, CancellationToken cancellationToken = default)
        {
            var policy = Policy.Handle<Exception>()
                   .WaitAndRetryAsync(
                            retryCount: 3,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retry}. Re-trying", exception.GetType().Name, exception.Message, retry);
                            });

            await policy.ExecuteAsync(async token =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await producer.ProduceAsync(topic, new Message<string, string> { Value = JsonConvert.SerializeObject(value) }, token);
                }
            }, cancellationToken);

            logger.LogInformation("Event published!");
            return true;
        }

        public void Dispose()
        {
            producer.Dispose();
        }
    }
}
