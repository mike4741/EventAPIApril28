using System.Collections.Generic;

namespace WebMvc.Models
{
    public class Event
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public List<EventItem> Data { get; set; }
    }
}
