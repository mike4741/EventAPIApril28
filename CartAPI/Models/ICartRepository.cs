using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Models
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> UpdateCartAsync(Cart basket);
        Task<bool> DeleteCaretAsync(string Id);
        IEnumerable<string> GetUsers();

    }
}
