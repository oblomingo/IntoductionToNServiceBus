namespace Logistics.Messages.Events
{
    public class PackageSent
    {
        public int OrderId { get; set; }
        public int PackageId { get; set; }
    }
}
