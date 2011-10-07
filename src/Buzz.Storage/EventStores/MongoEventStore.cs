using System;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;

namespace Buzz.Storage.EventStores
{
    public class MongoEventStore : IEventStore
    {
        public CommittedEventStream ReadFrom(Guid id, long minVersion, long maxVersion)
        {
            throw new NotImplementedException();
        }

        public void Store(UncommittedEventStream eventStream)
        {
            throw new NotImplementedException();
        }
    }
}