using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("CreateOrder/{cartId}/{userId}")]
        public async Task<IActionResult> CreateOrder([FromRoute] string cartId, [FromRoute] string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _orderService.Create(cartId, userId, cancellationToken);
            if (order.Status == false) return BadRequest(order.Message);
            return Ok(order);
        }


        [HttpGet("ViewOrdersByDate/{date}")]
        public async Task<IActionResult> ViewSalesByDate([FromRoute] string date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewOrders = await _orderService.GetByDate(date, cancellationToken);
            if (viewOrders.Status == false) return Ok(viewOrders);
            return Ok(viewOrders);
        }
        [HttpGet("GetAllOrders/{userId}")]
        public async Task<IActionResult> AllOrders([FromRoute]string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var orders = await _orderService.GetAll(userId, cancellationToken);
            if (orders.Status == false) return  BadRequest(orders);
            return Ok(orders);
        }
        [HttpGet("PendingOrders")]
        public async Task<IActionResult> PendingOrder(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var orders = await _orderService.GetNonApprovedOrders(cancellationToken);
            if (orders.Status == false) return BadRequest(orders);
            return Ok(orders);
        }
        [HttpGet("ApprovedOrders")]
        public async Task<IActionResult> ApprovedOrder(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var orders = await _orderService.GetAllApprovedPurchase(cancellationToken);
            if (orders.Status == false) return BadRequest(orders);
            return Ok(orders);
        }
        [HttpPost("ApproveOrder/{orderId}")]
        public async Task<IActionResult> ApprovePurchase([FromRoute] string orderId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _orderService.ApproveOrder(orderId, cancellationToken);
            if (order.Status == false) return BadRequest(order);
            return Ok(order);
        }
    }
}
