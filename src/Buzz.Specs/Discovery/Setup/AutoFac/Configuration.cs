using System;
using Autofac;
using Ncqrs.Config;

namespace Buzz.Specs.Discovery.Setup.AutoFac
{
    public class Configuration : IEnvironmentConfiguration
    {
        private readonly IContainer _container;

        public Configuration(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public bool TryGet<T>(out T result) where T : class
        {
            return _container.TryResolve(out result);
        }
    }
}