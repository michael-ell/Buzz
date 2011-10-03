using System;

namespace Buzz.Specs.Discovery.ReadModel
{
    public interface IIdentifiable
    {
        Guid Id { get; set; } 
    }
}