using Buzz.Specs.Discovery.Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Buzz.Specs.Discovery.ReadModel.Denormalizers
{
    public class CustomerDenormalizer : IEventHandler<CustomerAddedEvent>
    {
        public void Handle(CustomerAddedEvent evnt)
        {
            var customer = new Customer {Name = string.Format("{0},{1}", evnt.LastName, evnt.FirstName)};
        }
    }
}