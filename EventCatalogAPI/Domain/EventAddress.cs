namespace EventCatalogAPI.Domain
{
    public class EventAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string StreetAddress { get; set; }
    }
}
