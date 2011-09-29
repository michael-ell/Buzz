using System.Linq;
using Buzz.Domain;
using Buzz.Events;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Buzz.Specs.Domain
{
    [Binding]
    public class BuzzwordSteps : AggregateRootStepsBase<Buzzword>
    {
        private const string ExpectedWord = "ardvark";

        [Given(@"a source")]
        public void GivenASource()
        {
        }

        [When(@"a new buzzword is found")]
        public void WhenANewBuzzwordIsFound()
        {
            Sut = new Buzzword(ExpectedWord);
        }

        [Then(@"should announce that a buzzword was found")]
        public void ThenShouldAnnounceTheABuzzwordWasFound()
        {
            PublishedEvents.Should().HaveCount(1);
            PublishedEvents.Should().ContainItemsAssignableTo<BuzzWordFoundEvent>();
            PublishedEvents.First().As<BuzzWordFoundEvent>().Word.Should().Be(ExpectedWord);
        }
    }
}
