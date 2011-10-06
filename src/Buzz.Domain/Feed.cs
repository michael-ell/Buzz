using System;
using System.Linq;
using Buzz.Events;
using Ncqrs.Domain;

namespace Buzz.Domain
{
    public class Feed : AggregateRootMappedByConvention
    {
        private string _url;

        public void Register(string url, string description)
        {
            ApplyEvent(new FeedRegisteredEvent(url, description));
        }

        protected void OnFeedRegistered(FeedRegisteredEvent @event)
        {
            _url = @event.Url;
        }

        public void Process(IUrlParser parser, IEvaluator evaluator)
        {
            if (parser == null) throw new ArgumentNullException("parser");
            if (evaluator == null) throw new ArgumentNullException("evaluator");

            var words = parser.Parse(_url);
            var buzzwords = words.Where(evaluator.IsBuzzword);
            foreach (var buzzword in buzzwords)
            {
                ApplyEvent(new BuzzwordFoundEvent(buzzword));
            }
        }
    }
}