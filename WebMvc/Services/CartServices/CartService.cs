﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Models.OrderModels;
using WebMvc.Services.CartServices;


namespace WebMvc.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly IConfiguration _config;
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;
        public CartService(IConfiguration config, IHttpContextAccessor httpContextAccesor,
            IHttpClient httpClient, ILoggerFactory logger)
        {
            _config = config;
            _remoteServiceBaseUrl = $"{_config["CartUrl"]}/api/Cart";
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<CartService>();
        }


        public async Task AddItemToCart(ApplicationUser user, CartEvent events)
        {
            var cart = await GetCart(user);
          //  _logger.LogDebug("User Name: " + user.Email);

            var basketItem = cart.Events
                .Where(e => e.EventId == events.EventId)
                .FirstOrDefault();
            if (basketItem == null)
            {
                cart.Events.Add(events);
            }
            else
            {
                basketItem.Quantity += 1;
            }


            await UpdateCart(cart);
        }
      public   async Task<Cart> GetCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            //_logger.LogInformation(" We are in get basket and user id " + user.Email);
            //_logger.LogInformation(_remoteServiceBaseUrl);
            var getBasketUri = ApiPaths.Basket.GetBasket(_remoteServiceBaseUrl, user.Email);
            //_logger.LogInformation(getBasketUri);
            var dataString = await _apiClient.GetStringAsync(getBasketUri, token);
            //_logger.LogInformation(dataString);

            var response = JsonConvert.DeserializeObject<Cart>(dataString.ToString()) ??
               new Cart()
               {
                   BuyerId = user.Email
               };
            return response;

        }

        private async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }

        public async Task<Cart> UpdateCart(Cart Cart)
        {
            var token = await GetUserTokenAsync();
            var updateBasketUri = ApiPaths.Basket.UpdateBasket(_remoteServiceBaseUrl);
            var responce = await _apiClient.PostAsync(updateBasketUri, Cart, token);
            responce.EnsureSuccessStatusCode();
            return Cart;
        }

        public async Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var basket = await GetCart(user);
            basket.Events.ForEach(x =>
            {
                if (quantities.TryGetValue(x.Id, out var quantity))
                {
                    x.Quantity = quantity;
                }
            });
            return basket;
        }

        public Order MapCartToOrder(Cart cart)
        {
            var order = new Order();
            order.OrderTotal = 0;

            cart.Events.ForEach(x =>
            {
                order.OrderItems.Add(new OrderItem()
                {
                    EventId = int.Parse(x.EventId),

                    PictureUrl = x.PictureUrl,
                    EventName = x.EventName,
                    Units = x.Quantity,
                    UnitPrice = x.UnitPrice
                });
                order.OrderTotal += (x.Quantity * x.UnitPrice);
            });

            return order;
        }

        public async Task ClearCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var cleanBasketUri = ApiPaths.Basket.CleanBasket(_remoteServiceBaseUrl, user.Email);
            var response = await _apiClient.DeleteAsync(cleanBasketUri);

        }

        
    }
}
