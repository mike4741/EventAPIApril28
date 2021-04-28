using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventService : IEventService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;

        public EventService(IConfiguration config, IHttpClient client)
        {

            _baseUrl = $"{config["EventUrl"]}/api/EventItems/";
            // _baseUrl = $"{config["EventUrl"]}/api/Event/";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categoryUri = ApiPaths.Event.GetAllCategories(_baseUrl);
            var dataString = await _client.GetStringAsync(categoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var categories = JArray.Parse(dataString);
            foreach (var category in categories)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = category.Value<string>("id"),
                        Text = category.Value<string>("category")
                    });
            }
            return items;
        }

        public async Task<Event> GetEventItemsAsync(int page, int size, int? category, int? type, int? address)
        {
            var eventItemsUri = ApiPaths.Event.GetAllEventItems(_baseUrl, page, size, category, type, address);
            var dataString = await _client.GetStringAsync(eventItemsUri);
            return JsonConvert.DeserializeObject<Event>(dataString);
        }

        public async Task<IEnumerable<SelectListItem>> GetEventTypesAsync()
        {
            var typeUri = ApiPaths.Event.GetAllTypes(_baseUrl);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventAddressesAsync()
        {
            var eventAddressUri = ApiPaths.Event.GetAllEventAddresses(_baseUrl);
            var dataString = await _client.GetStringAsync(eventAddressUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value="0",
                    Text="All",
                    Selected = true
                }
            };
            var addresses = JArray.Parse(dataString);
            foreach (var address in addresses)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = address.Value<string>("id"),
                        Text = address.Value<string>("city")
                    });
            }
            return items;
        }
    }
}
