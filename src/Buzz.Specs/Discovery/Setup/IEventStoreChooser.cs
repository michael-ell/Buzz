using Ncqrs.Config;

namespace Buzz.Specs.Discovery.Setup
{
    public interface IEventStoreChooser
    {
        IEnvironmentConfiguration InMemoryEventStore();


        IEnvironmentConfiguration MongoEventStore();


        IEnvironmentConfiguration SqlServerEventStore();

    }
}