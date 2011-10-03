using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Serialization;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public class MongoDBEventStore : IEventStore
    {
        private readonly IStoreEvents _store;

        public MongoDBEventStore()
        {
            _store = Wireup.Init()
                           .LogToOutputWindow()
                           .UsingMongoPersistence("Mongo", new DocumentObjectSerializer())
                           .InitializeStorageEngine()
                           .Build();
        }

        public IEnumerable<SourcedEvent> GetAllEvents(Guid id)
        {
            using (var stream = _store.OpenStream(id, 0, int.MaxValue))
            {
                return stream.CommittedEvents.Select(e => e.Body as SourcedEvent);
            }
        }

        public IEnumerable<SourcedEvent> GetAllEventsSinceVersion(Guid id, long version)
        {
            throw new NotImplementedException();
        }

        public void Save(IEventSource source)
        {
            using (var stream = _store.OpenStream(source.EventSourceId, 0, int.MaxValue))
            {
                foreach (var uncommittedEvent in source.GetUncommittedEvents())
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }
                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}