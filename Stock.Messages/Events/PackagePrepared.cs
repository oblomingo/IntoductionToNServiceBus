using Stock.Messages.Dto;

namespace Stock.Messages.Events
{
    public class PackagePrepared
    {
        public int OrderId { get; set; }
        public int PackageId { get; set; }
        public PackageSize Size { get; set; }
    }
}
