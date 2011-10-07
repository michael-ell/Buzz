using System.Collections.Generic;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;

namespace Buzz.Specs
{
    public abstract class AggregateRootStepsBase<TRoot> where TRoot : AggregateRoot
    {
        private TRoot _sut;
        private readonly List<UncommittedEvent> _publishedEvents;

        protected IAggregateRootCreationStrategy CreationStrategy { get; set; }

        protected TRoot Sut
        {
            get
            {
                if (_sut != null) return _sut;

                _sut = CreateSut();
                return _sut;
            }
            set { _sut = value ?? CreateSut(); }
        }

        protected IEnumerable<UncommittedEvent> PublishedEvents
        {
            get { return _publishedEvents; }            
        }

        protected AggregateRootStepsBase()
        {
            CreationStrategy = new SimpleAggregateRootCreationStrategy();
            _publishedEvents = new List<UncommittedEvent>();
        }

        protected virtual TRoot CreateSut()
        {
            var root = CreationStrategy.CreateAggregateRoot<TRoot>();
            root.EventApplied += (s, e) => _publishedEvents.Add(e.Event);
            return root;
        }
    }
}