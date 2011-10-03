using System;
using System.Collections.Generic;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public interface IReadModelRepository<T>
    {
        IEnumerable<T> GetAll();
        void Save(T toSave);
        T GetById(Guid eventSourceId);
    }
}