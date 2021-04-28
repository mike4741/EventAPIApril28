using System.Collections.Generic;

namespace CartAPI.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartEvent> Events { get; set; }
        public Cart() { }


        public Cart(string cartId)
        {
            BuyerId = cartId;
            Events = new List<CartEvent>();
        }
    }
}
