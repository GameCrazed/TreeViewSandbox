namespace TreeViewSandbox.Entities
{
    public class AddressSelection
    {
        public int CustomerAddressId { get; set; }
        public int PropertySecurityAddressId { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
    }
}