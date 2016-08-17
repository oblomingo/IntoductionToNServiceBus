using NServiceBus;
using Stock.Messages.Dto;

namespace Logistic.Messages.Commands
{
    public class SendPackageCmd : ICommand
    {
        public int OrderId { get; set; }
        public int PackageId { get; set; }
        public int UserId { get; set; }
        public PackageSize Size { get; set; }
    }
}