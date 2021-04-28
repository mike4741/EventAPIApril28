
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMvc.Models.CartModels
{
    public class Cart
    {
        public List<CartEvent> Events { get; set; } = new List<CartEvent>();
        public string BuyerId { get; set; }

        public decimal Total()
        {
            return Math.Round(Events.Sum(x => x.UnitPrice * x.Quantity), 2);
        }

    }


}
