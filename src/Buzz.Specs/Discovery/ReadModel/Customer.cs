using System;

namespace Buzz.Specs.Discovery.ReadModel
{
    public class Customer : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}