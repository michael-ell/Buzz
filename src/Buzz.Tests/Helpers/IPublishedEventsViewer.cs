using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Tests.Helpers
{
    public interface IPublishedEventsViewer
    {
        IEnumerable<SourcedEvent> PublishedEvents { get; }
    }
}