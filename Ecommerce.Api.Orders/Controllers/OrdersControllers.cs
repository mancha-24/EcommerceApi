using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersControllers : ControllerBase
    {
         private readonly IOrdersProvider _ordersProvider; 
         public OrdersControllers(IOrdersProvider ordersProvider)
         {
            _ordersProvider = ordersProvider;
         }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await _ordersProvider.GetOrdersAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            } 

            return NotFound();
        }

       
    }
}