using System.Collections.Generic;
using System.Threading;
using Buzz.Domain;
using Buzz.Events;
using Buzz.Tests.BDD;
using Buzz.Tests.Helpers;

namespace Buzz.Tests.Domain.FeedSpecs
{
    [Concern(typeof (Feed))]
    public class When_registering_a_feed : AggregateRootSpecBase<Feed>
    {
        private string _expectedDescription;
        private string _expectedUrl;

        protected override void Given()
        {
            _expectedUrl = "www.xyz.com";
            _expectedDescription = "some description";
        }

        protected override void When()
        {
            Sut.Register(_expectedUrl, _expectedDescription);
        }

        [Observation]
        public void Then_should_announce_that_a_feed_was_registered()
        {
            Verify<FeedRegisteredEvent>(e => e.Url == _expectedUrl && e.Description ==_expectedDescription).WasPublished();
        }
    }

    [Concern(typeof (Feed))]
    public class When_processing_a_registered_feed : AggregateRootSpecBase<Feed>
    {
        private string _shouldBeBuzzword;
        private string _shouldNotBeBuzzword;
        private List<string> _wordsToReturn;

        protected override void Given()
        {
            const string url = "www.some.feed.com";
            Sut.Register(url, "some description");
            _shouldBeBuzzword = "Shillelagh";
            _shouldNotBeBuzzword = "Shenaniganz";
            _wordsToReturn = new List<string> { _shouldNotBeBuzzword, _shouldBeBuzzword };
            MockFor<IUrlParser>().Setup(parser => parser.Parse(url)).Returns(_wordsToReturn);
            MockFor<IEvaluator>().Setup(evaluator => evaluator.IsBuzzword(_shouldBeBuzzword)).Returns(true);
            MockFor<IEvaluator>().Setup(evaluator => evaluator.IsBuzzword(_shouldNotBeBuzzword)).Returns(false);
        }

        protected override void When()
        {
            Sut.Process(GetDependency<IUrlParser>(), GetDependency<IEvaluator>());
        }

        [Observation]
        public void Then_should_evaluate_all_words()
        {
            _wordsToReturn.ForEach(word => MockFor<IEvaluator>().Verify(evaluator => evaluator.IsBuzzword(word)));
        }

        [Observation]
        public void Then_should_announce_buzz_words()
        {
            Verify<BuzzwordFoundEvent>(e => e.Word == _shouldBeBuzzword).WasPublished();
        }

        [Observation]
        public void Then_should_not_announce_words_that_are_not_buzz_words()
        {
            Verify<BuzzwordFoundEvent>(e => e.Word == _shouldNotBeBuzzword).WasNotPublished();
        }
    }
}