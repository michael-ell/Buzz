using System;
using System.Collections.Generic;
using System.Linq;
using Buzz.Tests.BDD;
using FluentAssertions;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Tests.Helpers
{
    public abstract class AggregateRootSpecBase<TRoot> :  ContextBase<TRoot>, IPublishedEventsViewer where TRoot : AggregateRoot
    {
        protected IAggregateRootCreationStrategy CreationStrategy { get; set; }
        private readonly List<UncommittedEvent> _publishedEvents;

        public IEnumerable<UncommittedEvent> PublishedEvents
        {
            get { return _publishedEvents; }
        }

        protected AggregateRootSpecBase()
        {
            CreationStrategy = new SimpleAggregateRootCreationStrategy();
            _publishedEvents = new List<UncommittedEvent>();
        }

        //protected virtual IEnumerable<SourcedEvent> History()
        //{
        //    return new List<SourcedEvent>();
        //}

        protected override TRoot CreateSut()
        {
            var root = CreationStrategy.CreateAggregateRoot<TRoot>();
            root.EventApplied += (s, e) => _publishedEvents.Add(e.Event);
            //root.InitializeFromHistory(History());
            return root;
        }

        protected PublishedVerifyer<TEvent> Verify<TEvent>(Predicate<TEvent> wasPublished) where TEvent : SourcedEvent
        {
            return new PublishedVerifyer<TEvent>(wasPublished, this);
        }

        protected class PublishedVerifyer<TEvent> where TEvent : SourcedEvent
        {
            private readonly Predicate<TEvent> _predicate;
            private readonly IPublishedEventsViewer _viewer;

            public PublishedVerifyer(Predicate<TEvent> predicate, IPublishedEventsViewer viewer)
            {
                if (predicate == null) throw new ArgumentNullException("predicate");
                if (viewer == null) throw new ArgumentNullException("viewer");
                _predicate = predicate;
                _viewer = viewer;
            }

            private IEnumerable<TEvent> EventsOfInterest
            {
                get { return _viewer.PublishedEvents.Where(e => e.Payload.GetType().Equals(typeof (TEvent)))
                                                    .Select(e => e.Payload).Cast<TEvent>(); }
            }

            public void WasPublished()
            {
                var events = EventsOfInterest;
                events.Where(e => _predicate.Invoke(e)).Should().HaveCount(1);
            }

            public void WasNotPublished()
            {
                var events = EventsOfInterest;
                events.Where(e => _predicate.Invoke(e)).Should().HaveCount(0);                
            }
        }
    }
}