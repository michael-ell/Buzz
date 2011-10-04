using System;
using Ncqrs;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWorkContext CreateUnitOfWork()
        {
            if(UnitOfWork.Current != null) throw new InvalidOperationException("There is already a unit of work created for this context.");
            //return new UnitOfWork(new NonPublishingDomainRepository(NcqrsEnvironment.Get<IEventStore>()));
            return new UnitOfWork(NcqrsEnvironment.Get<IDomainRepository>());
        }
    }
}