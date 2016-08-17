using NServiceBus;

namespace Logistic.Messages.Events
{
    public class PackageSent : IEvent
    {
        public int OrderId { get; set; }
        public int PackageId { get; set; }
    }
}
