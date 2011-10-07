using System.Collections.Generic;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public class NonPublishingEventBus : IEventBus
    {
        public void Publish(IPublishableEvent eventMessage)
        {
            //let event store dispatch messages
        }

        public void Publish(IEnumerable<IPublishableEvent> eventMessages)
        {
            //let event store dispatch messages
        }
    }
}