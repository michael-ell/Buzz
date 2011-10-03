using System;
using Ncqrs.Commanding;

namespace Buzz.Specs.Discovery.Commands
{
    public class ChangeCustomerEmailCommand : CommandBase
    {
        public Guid EventSourceId { get; set; }
        public string NewEmail { get; set; }
    }
}