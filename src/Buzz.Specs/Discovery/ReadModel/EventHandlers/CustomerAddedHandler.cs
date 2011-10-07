using System;
using Buzz.Specs.Discovery.Events;
using Buzz.Specs.Discovery.Infrastructure;
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

        public void Handle(IPublishedEvent<CustomerAddedEvent> evnt)
        {
            var customer = new Customer { Id = evnt.EventSourceId, Name = string.Format("{0},{1}", evnt.Payload.LastName, evnt.Payload.FirstName), Email = evnt.Payload.Email };
            _repository.Save(customer);     
        }
    }
}