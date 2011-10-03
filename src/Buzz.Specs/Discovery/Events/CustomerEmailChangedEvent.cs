using Ncqrs.Eventing.Sourcing;

namespace Buzz.Specs.Discovery.Events
{
    public class CustomerEmailChangedEvent : SourcedEvent
    {
        public string NewEmail { get; set; }
    }
}