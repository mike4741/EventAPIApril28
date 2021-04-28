using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? page, int? categoryFilterApplied, int? typeFilterApplied,
            int? addressesFilterApplied)
        {
            var itemsOnPage = 10;

            var events = await _service.GetEventItemsAsync(page ?? 0, itemsOnPage, categoryFilterApplied, typeFilterApplied,
                addressesFilterApplied);

            if (null == events)
            {
                var vm1 = new EventIndexViewModel
                {
                    CategoryFilterApplied = categoryFilterApplied ?? 0,
                    TypeFilterApplied = typeFilterApplied ?? 0,
                    AddressesFilterApplied = addressesFilterApplied ?? 0,
                };
                return View(vm1);
            }
            var vm = new EventIndexViewModel
            {
                EventItems = events.Data,
                Categories = await _service.GetCategoriesAsync(),
                Types = await _service.GetEventTypesAsync(),
                Addresses = await _service.GetEventAddressesAsync(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = events.PageSize,
                    TotalItems = events.Count,
                    TotalPages = (int)Math.Ceiling((decimal)events.Count / itemsOnPage)
                },
                CategoryFilterApplied = categoryFilterApplied ?? 0,
                TypeFilterApplied = typeFilterApplied ?? 0,
                AddressesFilterApplied = addressesFilterApplied ?? 0,
            };

            return View(vm);
        }

        //[Authorize]
        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";


        //    return View();
        //}
    }
}