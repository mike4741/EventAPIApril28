using System.Collections.Generic;

namespace EventCatalogAPI.ViewModels
{
    public class PaginatedItemsViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
    }
}
