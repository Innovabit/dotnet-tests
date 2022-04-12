using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Innovabit.DotNet.Test.DependencyInjection
{
    public class FakeServiceCollection
    {
        private readonly IServiceCollection _serviceCollection;

        public FakeServiceCollection()
        {
            _serviceCollection = Mock.Of<IServiceCollection>();
        }

        public IServiceCollection Container => _serviceCollection;

        public void ContainsSingletonService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Singleton);
        }

        public void ContainsTransientService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Transient);
        }

        public void ContainsScopedService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Scoped);
        }

        private void IsRegistered<TService, TInstance>(ServiceLifetime lifetime)
        {
            var serviceCollectionMock = Mock.Get(_serviceCollection);

            serviceCollectionMock.Verify(serviceCollection => serviceCollection.Add(
                    It.Is<ServiceDescriptor>(serviceDescriptor => serviceDescriptor.ServiceType == typeof(TService) && serviceDescriptor.ImplementationType == typeof(TInstance) && serviceDescriptor.Lifetime == lifetime)));

        }
    }
}