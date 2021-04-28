using System;

namespace WebMvc.Models
{
    public class EventItem
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string EventImageUrl { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public string OrganizerName { get; set; }
        public string OrganzierPhoneNumber { get; set; }


        public int AddressId { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }

        public string Address { get; set; }
        public string EventCategory { get; set; }
        public string EventType { get; set; }
    }
}
