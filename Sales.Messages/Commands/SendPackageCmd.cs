using Stock.Messages.Dto;

namespace Sales.Messages.Commands
{
    public class SendPackageCmd
    {
        public int OrderId { get; set; }
        public int PackageId { get; set; }
        public int UserId { get; set; }
        public PackageSize Size { get; set; }
    }
}