using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;
using Ncqrs.Eventing;
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

        public CommittedEventStream ReadFrom(Guid id, long minVersion, long maxVersion)
        {
            using (var stream = _store.OpenStream(id, (int)minVersion, (int)maxVersion))
            {
                return ToCommitedEventStream(id, stream.CommittedEvents);
            }
        }

        public void Store(UncommittedEventStream eventStream)
        {
            using (var stream = _store.OpenStream(eventStream.SourceId, 0, int.MaxValue))
            {
                foreach (var uncommittedEvent in eventStream)
                {
                    var eventMessage = new EventMessage{ Body = uncommittedEvent.Payload };
                    eventMessage.Headers["commitId"] = eventStream.CommitId;
                    stream.Add(eventMessage);
                }
                //stream.CommitChanges(Guid.NewGuid());
                stream.CommitChanges(eventStream.CommitId);
            }
        }

        public void Dispatch(Commit commit)
        {
            _bus.Publish(ToCommitedEventStream(commit.StreamId, commit.Events));
        }

        public void Dispose()
        {
        }

        private CommittedEventStream ToCommitedEventStream(Guid sourceId, IEnumerable<EventMessage> events)
        {
            var committedEventStream = new CommittedEventStream(sourceId, events == null ? new List<CommittedEvent>() : events.Select(ToCommittedEvent));
            return committedEventStream;
        }

        private CommittedEvent ToCommittedEvent(EventMessage eventMessage)
        {
            var e = (SourcedEvent)eventMessage.Body;
            return new CommittedEvent((Guid) eventMessage.Headers["commitId"], e.EventIdentifier, e.EventSourceId, e.EventSequence, e.EventTimeStamp, e, e.EventVersion);
        }
    }
}