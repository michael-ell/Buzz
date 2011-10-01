using Buzz.Specs.Discovery.Commands;
using TechTalk.SpecFlow;

namespace Buzz.Specs.Discovery
{
    [Binding]
    public class EventStoreSteps : NcqrsStepsBase
    {
        private AddCustomerCommand _command;

        [Given(@"a new customer")]
        public void GivenANewCustomer()
        {
            _command = new AddCustomerCommand
                           {FirstName = "Halle", LastName = "Berry", Age = 45, Email = "berries@wow.com"};
        }

        [When(@"I save the customer")]
        public void WhenISaveTheCustomer()
        {
            Execute(_command);
        }

        [Then(@"new customer event should be stored")]
        public void ThenNewCustomerEventShouldBeStored()
        {

        }
    }
}
