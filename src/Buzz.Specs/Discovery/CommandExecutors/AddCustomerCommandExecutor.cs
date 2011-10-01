using Buzz.Specs.Discovery.Commands;
using Buzz.Specs.Discovery.Domain;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace Buzz.Specs.Discovery.CommandExecutors
{
    public class AddCustomerCommandExecutor : CommandExecutorBase<AddCustomerCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, AddCustomerCommand command)
        {
            new Customer(command.FirstName, command.LastName, command.Age, command.Email);
            context.Accept();
        }
    }
}