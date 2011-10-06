using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Buzz.Tests.Helpers
{
    public class NullEventHandler<T> : IEventHandler<T> where T : IEvent
    {
        public void Handle(T evnt)
        {
        }
    }
}