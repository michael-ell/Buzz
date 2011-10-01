using System;
using System.Collections.Generic;
using EventStore;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace Buzz.Storage.EventStores
{
    public class MongoEventStore : IEventStore
    {
        private IStoreEvents _store;

        public MongoEventStore()
        {
            ///_store = Wireup.Init().UsingMongoPersistence("", null).InitializeStorageEngine().
        }

        public IEnumerable<SourcedEvent> GetAllEvents(Guid id)
        {
            return null;
        }

        public IEnumerable<SourcedEvent> GetAllEventsSinceVersion(Guid id, long version)
        {
            return null;
        }

        public void Save(IEventSource source)
        {
            //using (_store)
            //{
            //    using(var stream = _store.CreateStream(source.EventSourceId))
            //    {
            //        stream.Add(new EventMessage{Body = source});
            //        stream.CommitChanges(source.);
            //    }
            //}
        }
    }
}