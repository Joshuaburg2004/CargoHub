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

        [HttpGet("warehouse/{warehouseid}")]
        public async Task<IActionResult> GetPickingOrdersForWarehouse([FromRoute] int warehouseid)
        {
            if (warehouseid < 1)
            {
                return BadRequest("Warehouse id should be a positive number.");
            }

            List<PickingOrder>? pickingorders = await _orderPickingService.GetPickingOrdersForWarehouse(warehouseid);

            if (pickingorders == null)
            {
                return BadRequest("No picking orders found for this warehouse.");
            }

            return Ok(pickingorders);
        }

        [HttpGet("order/{orderid}")]
        public async Task<IActionResult> GetPickingOrdersForOrder([FromRoute] int orderid)
        {
            if (orderid < 1)
            {
                return BadRequest("Order id should be a positive number.");
            }

            List<PickingOrder>? pickingorders = await _orderPickingService.GetPickingOrdersForOrder(orderid);

            if (pickingorders == null)
            {
                return BadRequest("No picking orders found for this order.");
            }

            return Ok(pickingorders);
        }

        [HttpPost("{orderid}")]
        public async Task<IActionResult> CreatePickingOrders([FromRoute] int orderid)
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
            // PickingOrder pickingorder = await _orderPickingService.CreatePickingOrder(order);
            bool pickingorder = await _orderPickingService.CreatePickingOrders(order, orderid);

            return Ok(pickingorder);
        }

        [HttpPut("{pickingorderid}")]
        public async Task<IActionResult> CompletePickingOrder([FromRoute] int pickingorderid)
        {
            if (pickingorderid < 1)
            {
                return BadRequest("Picking order id should be a positive number.");
            }

            bool pickingorder = await _orderPickingService.CompletePickingOrder(pickingorderid);

            if (!pickingorder)
            {
                return BadRequest("Picking order could not be completed.");
            }

            return Ok(pickingorder);
        }
    }
}