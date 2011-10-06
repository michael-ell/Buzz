using Ncqrs.Eventing.Sourcing;

namespace Buzz.Events
{
    public class FeedRegisteredEvent : SourcedEvent
    {
        public string Url { get; set; }
        public string Description { get; set; }

        public FeedRegisteredEvent(string url, string description)
        {
            Url = url;
            Description = description;
        }
    }
}