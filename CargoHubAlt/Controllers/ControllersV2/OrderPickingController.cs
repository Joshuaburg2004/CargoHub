using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHub.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/pickingorder")]
    public class OrderPickingControllerV2 : Controller
    {
        readonly IOrderPickingServiceV2 _orderPickingService;
        readonly IOrderServiceV2 _orderService;
        public OrderPickingControllerV2(IOrderPickingServiceV2 orderPickingService, IOrderServiceV2 orderService)
        {
            _orderPickingService = orderPickingService;
            _orderService = orderService;
        }

        [HttpGet("{orderid}")]
        public async Task<IActionResult> CreatePickingOrder(int orderid)
        {
            if (orderid < 1)
            {
                return BadRequest("Order id should be a positive number.");
            }

            // get a list of all ordered items;
            List<OrderedItem>? order = await _orderService.GetOrderedItems(orderid);

            if (order == null)
            {
                return BadRequest("This order does not exist.");
            }

            // Create a pickingorder => see PickingOrderService.cs
            PickingOrder pickingorder = await _orderPickingService.CreatePickingOrder(order);

            return Ok(pickingorder);
        }
    }
}