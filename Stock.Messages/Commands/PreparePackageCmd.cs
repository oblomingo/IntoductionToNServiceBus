using NServiceBus;

namespace Stock.Messages.Commands
{
    public class PreparePackageCmd : ICommand
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
