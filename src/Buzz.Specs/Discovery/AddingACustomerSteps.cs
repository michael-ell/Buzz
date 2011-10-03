using System.Linq;
using Buzz.Specs.Discovery.Commands;
using Buzz.Specs.Discovery.Domain;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Buzz.Specs.Discovery
{
    [Binding]
    public class AddingACustomerSteps : NcqrsStepsBase
    {
        private AddCustomerCommand _command;
        private string _expectedEmail;

        [Given(@"a new customer")]
        public void GivenANewCustomer()
        {
            _expectedEmail = "berries@wow.com";
            _command = new AddCustomerCommand { FirstName = "Halle", LastName = "Berry", Age = 45, Email = _expectedEmail };
        }

        [When(@"I save the customer")]
        public void WhenISaveTheCustomer()
        {
            Execute(_command);
        }

        [Then(@"the new customer should be viewable")]
        public void ThenTheNewCustomerShouldBeViewable()
        {
            var customer = GetAllReadModels<ReadModel.Customer>().SingleOrDefault(c => c.Email == _expectedEmail);
            customer.Should().NotBeNull();
        }

        [Then(@"should be able to rebuild the customer")]
        public void ThenShouldBeAbleToRebuildTheCustomer()
        {
            var readCustomer = GetAllReadModels<ReadModel.Customer>().Single(c => c.Email == _expectedEmail);
            var customer = GetDomainById<Customer>(readCustomer.Id);
            customer.MyEmail().Should().Be(_expectedEmail);
        }

        [Given(@"a customer that has already registered")]
        public void GivenACustomerThatHasAlreadyRegistered()
        {
            //Execute(CreateAddCustomerCommand());
        }

        private AddCustomerCommand CreateAddCustomerCommand()
        {
            return new AddCustomerCommand { FirstName = "Halle", LastName = "Berry", Age = 45, Email = "xxx@x.com" };
        }

        [When(@"the same customer is trying to be added")]
        public void WhenTheSameCustomerIsTryingToBeAdded()
        {
           // Execute(CreateAddCustomerCommand());
        }

        [Then(@"the customer should not be added twice")]
        public void ThenTheCustomerShouldNotBeAddedTwice()
        {
            //Is this logic done in Customer? If so, how to check eventStore?  Does customer consume services directly?
            //If not, is it done in the Command?
            //Do I let it go and just not update the readmodel repository, that is do the search / check in the event handler?
            //var customers = GetAllReadModels<ReadModel.Customer>().Where(c => c.Email == "xxx@x.com");
            //customers.Should().HaveCount(1);
        }
    }
}
