using System;
using Buzz.Specs.Discovery.Events;
using Buzz.Specs.Discovery.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Buzz.Specs.Discovery.ReadModel.EventHandlers
{
    public class CustomerEmailChangedHandler : IEventHandler<CustomerEmailChangedEvent>
    {
        private readonly IReadModelRepository<Customer> _repository;

        public CustomerEmailChangedHandler(IReadModelRepository<Customer> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Handle(IPublishedEvent<CustomerEmailChangedEvent> evnt)
        {
            var customer = _repository.GetById(evnt.EventSourceId);
            customer.Email = evnt.Payload.NewEmail;
            _repository.Save(customer);
        }
    }
}