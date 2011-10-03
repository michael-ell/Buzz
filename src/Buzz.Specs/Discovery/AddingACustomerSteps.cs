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
    }
}
