using System;
using System.Collections.Generic;
using Buzz.Specs.Discovery.Infrastructure;
using Buzz.Specs.Discovery.Setup;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;

namespace Buzz.Specs.Discovery
{
    public abstract class NcqrsStepsBase
    {
        protected NcqrsStepsBase()
        {
            if (!NcqrsEnvironment.IsConfigured)
            {
                NcqrsEnvironment.Configure(Using.Ninject().With().MongoEventStore());
            }
        }

        protected void Execute(ICommand command)
        {
            if (command == null) return;
            NcqrsEnvironment.Get<ICommandService>().Execute(command);
        }

        protected T GetDomainById<T>(Guid eventSourceId) where T : AggregateRoot
        {
            var factory = NcqrsEnvironment.Get<IUnitOfWorkFactory>();
            T domain;
            using (var uow = factory.CreateUnitOfWork())
            {
                domain = uow.GetById<T>(eventSourceId);
            }
            return domain;
        }

        protected IEnumerable<T> GetAllReadModels<T>()
        {
            return NcqrsEnvironment.Get<IReadModelRepository<T>>().GetAll();
        }
    }
}