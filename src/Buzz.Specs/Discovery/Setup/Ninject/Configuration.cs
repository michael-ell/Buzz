using Buzz.Specs.Discovery.CommandExecutors;
using Buzz.Specs.Discovery.Infrastructure;
using Buzz.Specs.Discovery.ReadModel;
using Buzz.Specs.Discovery.ReadModel.EventHandlers;
using Ncqrs;
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
            kernel.Bind<IUnitOfWorkFactory>().To<Infrastructure.UnitOfWorkFactory>();
            //kernel.Bind<IUnitOfWorkFactory>().To<Ncqrs.Domain.UnitOfWorkFactory>();
            kernel.Bind<IEventBus>().ToProvider<EventBusProvider>();            
            kernel.Bind<ICommandService>().ToProvider<CommandServiceProvider>();
            kernel.Bind<IReadModelRepository<Customer>>().To<InMemoryRepository<Customer>>().InSingletonScope();
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
                //TODO: use the factory method below...
               //bus.RegisterAllHandlersInAssembly(Assembly.GetExecutingAssembly());
                bus.RegisterHandler(new CustomerAddedHandler(context.Kernel.Get<IReadModelRepository<Customer>>()));
                bus.RegisterHandler(new CustomerEmailChangedHandler(context.Kernel.Get<IReadModelRepository<Customer>>()));
                return bus;
            }
        }

        private class CommandServiceProvider : Provider<ICommandService>
        {
            protected override ICommandService CreateInstance(IContext context)
            {
                var service = new CommandService();
                service.RegisterExecutor(new AddCustomerCommandExecutor());
                service.RegisterExecutor(new ChangeCustomerEmailCommandExecutor());
                return service;
            }
        }
    }
}