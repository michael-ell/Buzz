using Ncqrs.Commanding;

namespace Buzz.Specs.Discovery.Commands
{
    public class AddCustomerCommand : CommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}