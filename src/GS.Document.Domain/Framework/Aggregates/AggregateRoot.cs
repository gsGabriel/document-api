using GS.Document.Domain.Framework.Events;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GS.Document.Domain.Framework.Aggregates
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _domainEvents;
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.ToImmutableList();

        protected AggregateRoot()
        {
            _domainEvents = new List<DomainEvent>();
        }

        /// <summary>
        /// Raise new domain event
        /// </summary>
        /// <param name="event">Domain Event</param>
        protected void Raise(DomainEvent @event)
        {
            _domainEvents.Add(@event);
        }
    }
}