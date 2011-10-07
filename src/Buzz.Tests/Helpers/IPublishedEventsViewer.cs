using System.Collections.Generic;
using Ncqrs.Eventing;

namespace Buzz.Tests.Helpers
{
    public interface IPublishedEventsViewer
    {
        IEnumerable<UncommittedEvent> PublishedEvents { get; }
    }
}