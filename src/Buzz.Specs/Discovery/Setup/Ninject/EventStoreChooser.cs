﻿using System;
using Buzz.Specs.Discovery.Infrastructure;
using Ncqrs.Config;
using Ncqrs.Domain.Storage;
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
            //_configuration.Kernel.Bind<IDomainRepository>().To<NonPublishingDomainRepository>();
            _configuration.Kernel.Bind<IEventStore>().To<MongoDBEventStore>().InSingletonScope();
            return _configuration;
        }

        public IEnvironmentConfiguration SqlServerEventStore()
        {
            throw new NotImplementedException();
        }
    }
}