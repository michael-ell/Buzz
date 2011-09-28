using Buzz.Domain;
using Buzz.Tests.Infrastructure;
using FluentAssertions;

namespace Buzz.Tests.Domain.BuzzWordSpecs
{
    [Concern(typeof (BuzzWord))]
    public class When_something_goes_bump_in_the_night : ContextBase<BuzzWord>
    {

        protected override void When()
        {
        }

        [Observation]
        public void Then_I_should_be_scared()
        {
        }
    }
}