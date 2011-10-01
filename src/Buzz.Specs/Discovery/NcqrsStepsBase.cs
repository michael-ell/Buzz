using System;
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
            NcqrsEnvironment.Configure(Using.Ninject().With().InMemoryEventStore());
        }

        protected void Execute(ICommand command)
        {
            if (command == null) return;
            NcqrsEnvironment.Get<ICommandService>().Execute(command);
        }

        protected T GetById<T>(Guid id) where T : AggregateRoot
        {
            return NcqrsEnvironment.Get<IUnitOfWorkContext>().GetById<T>(id);
        }
    }
}