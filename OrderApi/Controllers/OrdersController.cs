using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using OrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _ordersContext;
        private readonly ILogger<OrdersController> _logger;
       // private readonly IConfiguration _config;
        public OrdersController(OrdersContext ordersContext ,
                      ILogger<OrdersController> logger)
                     // IConfiguration config)
        {
          //  _config = config;
            _logger = logger;
            _ordersContext = ordersContext;

        }

        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;
            _logger.LogInformation("order" + order.userName);

            _ordersContext.Orders.Add(order);
            _ordersContext.OrderItems.AddRange(order.OrderItems);
            try
            {
                await _ordersContext.SaveChangesAsync();
                return Ok(new { order.OrderId });// dynamically create object   unanimous 
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("error occored during order saving ...", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name ="GetOrder")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrder( int id)
        {
            //include gives you  a joint using link query  statement 
            var item = await _ordersContext.Orders
                .Include(x => x.OrderItems)
                .SingleOrDefaultAsync(ci => ci.OrderId == id);
            if(item!=null)
                    {
                return Ok(item);
                    }
            return NotFound();
                 
        }

        // to get all the order it is not  implemented  everytime 
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrder()
        {
            //include gives you  a joint using link query  statement 
            var order = await _ordersContext.Orders.ToListAsync();
               
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound();

        }


    }
}
