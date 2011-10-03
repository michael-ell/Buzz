using System;
using Buzz.Specs.Discovery.Events;
using Buzz.Specs.Discovery.Infrastructure;
using Buzz.Specs.Discovery.Setup;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Buzz.Specs.Discovery.ReadModel.EventHandlers
{
    public class CustomerAddedHandler : IEventHandler<CustomerAddedEvent>
    {
        private readonly IReadModelRepository<Customer> _repository;

        public CustomerAddedHandler(IReadModelRepository<Customer> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Handle(CustomerAddedEvent evnt)
        {
            var customer = new Customer {Id = evnt.EventSourceId, Name = string.Format("{0},{1}", evnt.LastName, evnt.FirstName), Email = evnt.Email};
            _repository.Save(customer);            
        }
    }
}