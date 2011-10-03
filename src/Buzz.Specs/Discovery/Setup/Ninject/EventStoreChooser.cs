using System;
using Ncqrs.Config;
using Ncqrs.Eventing.Storage;

namespace Buzz.Specs.Discovery.Setup.Ninject
{
    public class EventStoreChooser : IEventStoreChooser
    {
        private readonly Configuration _configuration;

        public EventStoreChooser(Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            _configuration = configuration;
        }

        public IEnvironmentConfiguration InMemoryEventStore()
        {
            _configuration.Kernel.Bind<IEventStore>().To<InMemoryEventStore>().InSingletonScope();
            return _configuration;         
        }

        public IEnvironmentConfiguration MongoEventStore()
        {
            throw new NotImplementedException();
        }

        public IEnvironmentConfiguration SqlServerEventStore()
        {
            throw new NotImplementedException();
        }
    }
}