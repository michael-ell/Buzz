using System.Collections.Generic;
using Buzz.Tests.BDD;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Sourcing;

namespace Buzz.Tests.BDD
{
    public abstract class AggregateRootSpecBase<TRoot> : ContextBase<TRoot> where TRoot : AggregateRoot
    {
        protected IAggregateRootCreationStrategy CreationStrategy { get; set; }

        protected IEnumerable<SourcedEvent> PublishedEvents
        {
            get { return Sut.GetUncommittedEvents() ?? new List<SourcedEvent>(); }
        }

        protected AggregateRootSpecBase()
        {
            CreationStrategy = new SimpleAggregateRootCreationStrategy(); 
        }

        //protected virtual IEnumerable<SourcedEvent> History()
        //{
        //    return new List<SourcedEvent>();
        //}

        protected override TRoot CreateSut()
        {
            var root = CreationStrategy.CreateAggregateRoot<TRoot>();
            //root.InitializeFromHistory(History());
            return root;
        }
    }
}