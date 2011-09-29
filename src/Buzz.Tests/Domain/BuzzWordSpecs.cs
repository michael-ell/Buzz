using System.Linq;
using Buzz.Domain;
using Buzz.Events;
using Buzz.Tests.BDD;
using FluentAssertions;

namespace Buzz.Tests.Domain.BuzzwordSpecs
{
    [Concern(typeof (Buzzword))]
    public class When_a_new_buzzword_is_found: AggregateRootSpecBase<Buzzword>
    {
        private const string ExpectedWord = "ardvark";

        protected override Buzzword CreateSut()
        {
            return new Buzzword(ExpectedWord);
        }

        protected override void When()
        {
            //Done when the buzzword is created
        }

        [Observation]
        public void Then_should_announce_that_a_buzz_word_was_found()
        {
            PublishedEvents.Should().HaveCount(1);
            PublishedEvents.Should().ContainItemsAssignableTo<BuzzWordFoundEvent>();
            PublishedEvents.First().As<BuzzWordFoundEvent>().Word.Should().Be(ExpectedWord);
        }
    }
}