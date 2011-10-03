using Buzz.Specs.Discovery.Commands;
using Buzz.Specs.Discovery.Domain;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace Buzz.Specs.Discovery.CommandExecutors
{
    public class ChangeCustomerEmailCommandExecutor : CommandExecutorBase<ChangeCustomerEmailCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, ChangeCustomerEmailCommand command)
        {
            var customer = context.GetById<Customer>(command.EventSourceId);
            customer.ChangeEmail(command.NewEmail);
            context.Accept();
        }
    }
}