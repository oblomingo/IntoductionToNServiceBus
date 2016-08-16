using Logistic.Deliveries.Infrastructure;
using Logistics.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Commands;

namespace Logistic.Deliveries.Application
{
    public class DeliveryService : IHandleMessages<SendPackageCmd>
    {
        private readonly ILog _log;
        private readonly CustomerRepository _customerRepository;
        public DeliveryService()
        {
            _customerRepository = new CustomerRepository();
            _log = LogManager.GetLogger(typeof(DeliveryService));
        }
        public IBus Bus { get; set; }
        public void Handle(SendPackageCmd message)
        {
            var userAdresss = _customerRepository.GetUserAddress(message.UserId);
            ShippingProvider.SendPackage(message.PackageId);
            _log.InfoFormat("Package {0} sent to address {1}", message.PackageId, userAdresss);

            var packageSent = new PackageSent
            {
                OrderId = message.OrderId,
                PackageId = message.PackageId
            };
            Bus.Send(packageSent);
        }
    }
}
