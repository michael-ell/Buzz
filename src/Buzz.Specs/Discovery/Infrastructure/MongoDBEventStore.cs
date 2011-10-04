using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public class MongoDBEventStore : IEventStore, IDispatchCommits
    {
        private readonly IEventBus _bus;
        private readonly IStoreEvents _store;

        public MongoDBEventStore(IEventBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            _bus = bus;
            _store = Wireup.Init()
                           .LogToOutputWindow()
                           .UsingMongoPersistence("Mongo", new DocumentObjectSerializer())
                           .InitializeStorageEngine()
                           .UsingSynchronousDispatchScheduler(this)
                           .Build();
        }

        public IEnumerable<SourcedEvent> GetAllEvents(Guid id)
        {
            using (var stream = _store.OpenStream(id, 0, int.MaxValue))
            {
                return ToSourcedEvents(stream.CommittedEvents);
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

        public void Dispatch(Commit commit)
        {
            _bus.Publish(ToSourcedEvents(commit.Events));
        }

        public void Dispose()
        {
        }

        private IEnumerable<SourcedEvent> ToSourcedEvents(IEnumerable<EventMessage> events)
        {
            return events == null ? new List<SourcedEvent>() : events.Select(e => e.Body as SourcedEvent);
        }
    }
}