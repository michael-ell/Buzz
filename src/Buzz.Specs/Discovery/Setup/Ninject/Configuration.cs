using System.Reflection;
using Buzz.Specs.Discovery.CommandExecutors;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ninject;
using Ninject.Activation;

namespace Buzz.Specs.Discovery.Setup.Ninject
{
    public class Configuration : IEnvironmentConfiguration, ISetup
    {
        public IKernel Kernel { get; private set; }

        public Configuration()
        {
            Kernel = CreateKernel();
        }

        private IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IEventBus>().ToProvider<EventBusProvider>();
            kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            kernel.Bind<ICommandService>().ToProvider<CommandServiceProvider>();
            return kernel;
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = Kernel.TryGet<T>();
            return result != null;
        }

        public IEventStoreChooser With()
        {
            return new EventStoreChooser(this);
        }

        private class EventBusProvider : Provider<IEventBus>
        {
            protected override IEventBus CreateInstance(IContext context)
            {
                var bus = new InProcessEventBus();
                bus.RegisterAllHandlersInAssembly(Assembly.GetExecutingAssembly());
                return bus;
            }
        }

        private class CommandServiceProvider : Provider<ICommandService>
        {
            protected override ICommandService CreateInstance(IContext context)
            {
                var service = new CommandService();
                service.RegisterExecutor(new AddCustomerCommandExecutor());
                return service;
            }
        }
    }
}