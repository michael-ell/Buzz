using System;
using System.Collections.Generic;
using System.Linq;
using Buzz.Specs.Discovery.ReadModel;

namespace Buzz.Specs.Discovery.Infrastructure
{
    public class InMemoryRepository<T> : IReadModelRepository<T> where T : IIdentifiable
    {
        private readonly List<T> _store;

        public InMemoryRepository()
        {
            _store = new List<T>();
        }

        public void Save(T toSave)
        {
            if (!_store.Contains(toSave))
                _store.Add(toSave);
        }

        public T GetById(Guid id)
        {
            return _store.Where(entity => entity.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _store;
        }
    }
}