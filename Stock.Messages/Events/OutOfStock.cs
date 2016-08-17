using NServiceBus;

namespace Stock.Messages.Events
{
    public class OutOfStock : IEvent
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
