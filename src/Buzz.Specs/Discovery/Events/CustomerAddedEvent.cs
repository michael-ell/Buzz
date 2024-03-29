﻿using Ncqrs.Eventing.Sourcing;

namespace Buzz.Specs.Discovery.Events
{
    public class CustomerAddedEvent : SourcedEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }         
    }
}