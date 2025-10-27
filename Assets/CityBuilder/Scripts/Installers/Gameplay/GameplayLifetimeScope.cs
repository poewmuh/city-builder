using CityBuilder.Infastructure.Input;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Installers.Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var messagePipeOptions = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<object>(messagePipeOptions);

            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}