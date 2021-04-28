using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventItemsController : ControllerBase
    {
        private readonly EventContext _context;
        private readonly IConfiguration _config;
        public EventItemsController(EventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //Sorts event by month
        [HttpGet]
        [Route("[action]/{month}")]
        public async Task<IActionResult> FilterByMonth(int? month,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (month.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Month == month);
            }

            var eventsCount = await query.LongCountAsync();
            var events = await query
                    .OrderBy(t => t.EventName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangeImageUrl(events);

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount,
                Data = events
            };

            return Ok(model);
        }

        //filters events by specific date
        [HttpGet]
        [Route("[action]/{day}-{month}-{year}")]
        public async Task<IActionResult> FilterByDate(int? day, int? month, int? year,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 5)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (day.HasValue && month.HasValue && year.HasValue)
            {
                query = query.Where(e => e.EventStartTime.Day == day)
                             .Where(e => e.EventStartTime.Month == month)
                             .Where(e => e.EventStartTime.Year == year);
            }
            var eventsCount = await query.LongCountAsync();
            var events = await query
                    .OrderBy(t => t.EventName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangeImageUrl(events);

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount,
                Data = events
            };

            return Ok(model);
        }

        //Stays the same for adding webmvc proj
        [HttpGet("[action]")]
        public async Task<IActionResult> EventTypes()
        {
            var types = await _context.EventTypes.ToListAsync();
            return Ok(types);
        }

        //EventTypes Filter
        [HttpGet("[action]/{eventTypeId}")]
        public async Task<IActionResult> EventTypes(
           int? eventTypeId,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 5)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (eventTypeId.HasValue)
            {
                query = query.Where(t => t.TypeId == eventTypeId);
            }

            var eventsCount = await query.LongCountAsync();
            var events = await query
                    .OrderBy(t => t.EventName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangeImageUrl(events);

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount,
                Data = events
            };

            return Ok(model);

        }

        //Stays the same for adding webmvc proj 
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var events = await _context.EventCategories.ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("[action]/{eventCategoryId}")]
        public async Task<IActionResult> EventCategories(int? eventCategoryId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 4)

        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (eventCategoryId.HasValue)

            {
                query = query.Where(c => c.CategoryId == eventCategoryId);
            }

            var eventsCount = await query.LongCountAsync();
            var events = await query

                    .OrderBy(c => c.EventCategory)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            events = ChangeImageUrl(events);

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Count = eventsCount,
                Data = events
            };

            return Ok(model);
        }

        //Stays the same for adding webmvc proj
        [HttpGet("[action]")]
        public async Task<IActionResult> Addresses()
        {
            var addresses = await _context.Addresses.ToListAsync();
            return Ok(addresses);
        }

        //Addresses Filter
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> Addresses(
            string city,
           [FromQuery] int pageIndex = 0,
           [FromQuery] int pageSize = 4)

        {

            if (city != null && city.Length != 0)
            {
                var query = from eventItem in _context.EventItems
                            join address in _context.Addresses
                            on eventItem.AddressId equals address.Id
                            where address.City == city

                            select new EventItem
                            {
                                Id = eventItem.Id,
                                Address = eventItem.Address,
                                EventName = eventItem.EventName,
                                Description = eventItem.Description,
                                Price = eventItem.Price,
                                EventImageUrl = eventItem.EventImageUrl.Replace("http://externalcatalogbaseurltobereplaced",
                                _config["ExternalCatalogBaseUrl"]),
                                EventStartTime = eventItem.EventStartTime,
                                EventEndTime = eventItem.EventEndTime,
                                TypeId = eventItem.TypeId,
                                CategoryId = eventItem.CategoryId


                            };
                var eventItemsCount = await query.LongCountAsync();
                var events = await query

                        .OrderBy(c => c.Id)
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                var model = new PaginatedItemsViewModel<EventItem>
                {
                    PageIndex = pageIndex,
                    PageSize = events.Count,
                    Count = eventItemsCount,
                    Data = events
                };
                return Ok(model);
            }
            return Ok();
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
              [FromQuery] int pageIndex = 0,
              [FromQuery] int pageSize = 4)
        {
            var itemsCount = await _context.EventItems.LongCountAsync();

            var items = await _context.EventItems
                .OrderBy(e => e.EventStartTime.Date)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            items = ChangeImageUrl(items);

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items
            };
            return Ok(model);
        }
        private List<EventItem> ChangeImageUrl(List<EventItem> items)
        {
            items.ForEach(item =>
               item.EventImageUrl = item.EventImageUrl.
               Replace("http://externaleventbaseurltoberplaced",
              _config["ExternalCatalogBaseUrl"]));
            return items;
        }

        // filter based on the catagory or type or  address or all three  ..
        [HttpGet("[action]/category/{categoryId}/type/{typeId}/address/{eventAddressId}")]
        public async Task<IActionResult> Items(
                       int? categoryId,
                       int? typeId,
                       int? eventAddressId,
                       [FromQuery] int pageIndex = 0,
                       [FromQuery] int pagesize = 6)
        {

            var query = (IQueryable<EventItem>)_context.EventItems;
            if (categoryId > 0)
            {
                query = query.Where(i => i.CategoryId == categoryId);
            }
            if (typeId > 0)
            {
                query = query.Where(i => i.TypeId == typeId);
            }
            if (eventAddressId > 0)
            {
                query = query.Where(c => c.AddressId == eventAddressId);
            }
            var itemCount = await query.LongCountAsync();
            var result = await query
                              .OrderBy(s => s.EventName)
                              .Skip(pageIndex * pagesize)
                              .Take(pagesize)
                              .ToListAsync();
            result = ChangeImageUrl(result);
            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = result.Count,
                Count = itemCount,
                Data = result
            };


            return Ok(model);


        }

    }
}





