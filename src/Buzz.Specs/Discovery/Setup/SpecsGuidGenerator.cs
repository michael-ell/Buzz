using System;
using Ncqrs;

namespace Buzz.Specs.Discovery.Setup
{
    public class SpecsGuidGenerator : IUniqueIdentifierGenerator
    {
        public Guid GenerateNewId()
        {
            Guid guid = Guid.NewGuid();

            return guid;
        }
    }
}