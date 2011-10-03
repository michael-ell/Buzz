using Buzz.Specs.Discovery.Events;
using Ncqrs.Domain;

namespace Buzz.Specs.Discovery.Domain
{
    public class Customer : AggregateRootMappedByConvention
    {       
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _email;

        public Customer()
        {            
        }

        public Customer(string firstName, string lastName, int age, string email)
        {
            var e = new CustomerAddedEvent {Age = age, Email = email, FirstName = firstName, LastName = lastName};
            ApplyEvent(e);
        }

        protected void OnCustomerAdded(CustomerAddedEvent e)
        {
            _firstName = e.FirstName;
            _lastName = e.LastName;
            _age = e.Age;
            _email = e.Email;
        }

        public string MyEmail()
        {
            return _email;
        }

        public void ChangeEmail(string newEmail)
        {
            var e = new CustomerEmailChangedEvent {NewEmail = newEmail};
            ApplyEvent(e);
        }

        protected void OnCustomerEmailChanged(CustomerEmailChangedEvent e)
        {
            _email = e.NewEmail;
        }
    }
}