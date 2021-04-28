using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Services;
using WebMvc.Services.CartServices;

namespace WebMvc.Controllers.CartController
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IIdentityService<ApplicationUser> _identityService;
        private readonly IEventService _eventService;
        public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, IEventService eventService)
        {
            _identityService = identityService;
            _cartService = cartService;
            _eventService = eventService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index( Dictionary<string, int> quantities, string action)
        {
            if (action == "[ Checkout ]")
            {
                return RedirectToAction("Create", "Order");
            }

            try
            {
                var user = _identityService.Get(HttpContext.User);
                var basket = await _cartService.SetQuantities(user, quantities);
                var vm = await _cartService.UpdateCart(basket);
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit  mode                 
                HandleBrokenCircuitException();
            }

            return View();

        }

        public async Task<IActionResult> AddToCart(EventItem productDetails)
        {
            try
            {
                if (productDetails.Id> 0)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new CartEvent()
                    {
                        Id = Guid.NewGuid().ToString(), // global user identifier 
                        Quantity = 1,
                        EventName = productDetails.EventName,
                        PictureUrl = productDetails.EventImageUrl,
                        UnitPrice = productDetails.Price,
                        EventId = productDetails.Id.ToString()
                    };
                    await _cartService.AddItemToCart(user, product);
                }
                return RedirectToAction("Index", "Event");
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }

            return RedirectToAction("Index", "Event");

        }

        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }
    }
}
