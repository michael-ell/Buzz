using System.Linq;
using Buzz.Specs.Discovery.Commands;
using Buzz.Specs.Discovery.Domain;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Buzz.Specs.Discovery
{
    [Binding]
    public class ChangingACustomersEmailSteps : NcqrsStepsBase
    {
        private string _originalEmail;
        private string _expectedNewEmail;

        [Given(@"a customer")]
        public void GivenACustomer()
        {
            _originalEmail = "the.berries@wow.com";
            var command = new AddCustomerCommand
                              {FirstName = "Halle", LastName = "Berry", Age = 45, Email = _originalEmail};
            Execute(command);
        }

        [When(@"I change the email address for the customer")]
        public void WhenIChangeTheEmailAddressForTheCustomer()
        {
            var customer = GetAllReadModels<ReadModel.Customer>().Single(c => c.Email == _originalEmail);
            _expectedNewEmail = "halle.berries@wow.com";
            var command = new ChangeCustomerEmailCommand
                              {EventSourceId = customer.Id, NewEmail = _expectedNewEmail};
            Execute(command);
        }

        [Then(@"the new customer should have the new email address")]
        public void ThenTheNewCustomerShouldHaveTheNewEmailAddress()
        {
            var readCustomer = GetAllReadModels<ReadModel.Customer>().Single(c => c.Email == _expectedNewEmail);
            var customer = GetDomainById<Customer>(readCustomer.Id);
            customer.MyEmail().Should().Be(_expectedNewEmail);
        }
    }
}
