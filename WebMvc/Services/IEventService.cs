using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface IEventService
    {
        Task<Event> GetEventItemsAsync(int page, int size, int? category, int? type, int? address);
        Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetEventTypesAsync();
        Task<IEnumerable<SelectListItem>> GetEventAddressesAsync();

    }
}
