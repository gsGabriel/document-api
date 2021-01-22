using Confluent.Kafka;
using GS.Document.Domain.Entities;
using GS.Document.Domain.Events;
using GS.Document.Domain.ValueObjects;
using GS.Document.Infra.Kafka.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace GS.Document.Infra.Kafka.Tests
{
    public class EventProducerIntegrationTest
    {
        private readonly IEventProducer producer;

        public EventProducerIntegrationTest()
        {
            producer = new EventProducer(new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            }, LoggerFactory.Create(x => x.AddConsole()).CreateLogger<EventProducer>());
        }

        [Fact(Skip = "Dependency needed")]
        public async void Should_produce_event()
        {
            //given
            var domainEvent = DocumentCreated.CreateFrom(Documents.CreateFrom(CustomerId.From(Guid.NewGuid()), "teste", "teste", "teste"));

            //when
            var result = await producer.ProduceAsync<DocumentCreated>("customer-document-created", domainEvent);

            //then
            Assert.True(result);
        }
    }
}
