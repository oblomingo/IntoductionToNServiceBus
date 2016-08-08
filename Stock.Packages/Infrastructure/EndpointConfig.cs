
using NServiceBus;

namespace Stock.Packages.Infrastructure
{
    [EndpointName("Stock.Packages")]
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EnableInstallers();
        }
    }
}
