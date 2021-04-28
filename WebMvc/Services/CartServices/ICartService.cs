using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Models.OrderModels;

namespace WebMvc.Services.CartServices
{
    public interface ICartService
    {

        Task<Cart> GetCart(ApplicationUser user);
        Task AddItemToCart(ApplicationUser user, CartEvent events);
        Task<Cart> UpdateCart(Cart Cart);
        Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Order MapCartToOrder(Cart Cart);
        Task ClearCart(ApplicationUser user);



    }
}
