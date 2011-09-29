using System.Collections.Generic;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Specs
{
    public abstract class AggregateRootStepsBase<TRoot> where TRoot : AggregateRoot
    {
        private TRoot _sut;

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

        protected IEnumerable<SourcedEvent> PublishedEvents
        {
            get { return Sut.GetUncommittedEvents() ?? new List<SourcedEvent>(); }
        }

        public AggregateRootStepsBase()
        {
                CreationStrategy = new SimpleAggregateRootCreationStrategy();
        }

        protected virtual TRoot CreateSut()
        {
            return CreationStrategy.CreateAggregateRoot<TRoot>();
        }
    }
}