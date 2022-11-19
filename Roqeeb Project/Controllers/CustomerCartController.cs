using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCartController : ControllerBase
    {
        private readonly ICustomerCartService _customerCartService;
        private readonly IProductService _productService;
        public CustomerCartController(ICustomerCartService customerCartService, IProductService productService)
        {
            _customerCartService = customerCartService;
            _productService = productService;
        }
        [HttpGet("GetCustomerCart/{userId}")]
        public async Task<IActionResult> GetAvailableCustomerCart([FromRoute]string userId, CancellationToken cancellationToken)
        {
            var cart = await _customerCartService.GetByStatus(userId, cancellationToken);
            if (cart.Status == false) return BadRequest(cart);
            return Ok(cart);
        }
        [HttpPost("AddToCustomerCart/{userId}")]
        public async Task<IActionResult> AddToCart([FromBody] AddOrdersToCartRequestModel request, [FromRoute]string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByName(request.ProductName, cancellationToken);
            var cart = await _customerCartService.GetByStatus(userId, cancellationToken);
            if (cart.Status == false)
            {
                BaseResponse<CustomerCartDTO> newCart = null;
                newCart = await _customerCartService.CreateCart(userId, cancellationToken);
                var addCart = await _customerCartService.AddToCart(request, newCart.Data.Id, cancellationToken);
                return Ok(addCart);
            }
            var addToCart = await _customerCartService.AddToCart(request, cart.Data.Id, cancellationToken);
            return Ok(addToCart);

        }
        //[HttpPut("EditCart/{cartId}/{ProductName}/{sellingPrice}/{quantity}")]
        //public async Task<IActionResult> EditCart([FromRoute] string cartId, [FromRoute] string ProductName, [FromRoute] double sellingPrice, [FromRoute] int quantity, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    var editCart = await _customerCartService.(cartId, ProductName, sellingPrice, quantity, cancellationToken);
        //    if (editCart.Status == false) return BadRequest(editCart);
        //    return Ok(editCart);
        //}
        [HttpDelete("DeleteProductCart/{cartId}/{ProductName}")]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] string cartId, [FromRoute] string ProductName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var editCart = await _customerCartService.DeleteUpdateCart(cartId, ProductName, cancellationToken);
            if (editCart.Status == false) return BadRequest(editCart);
            return Ok(editCart);
        }
        [HttpDelete("DeleteAllProductCart/{cartId}")]
        public async Task<IActionResult> RemoveAllProductFromCart([FromRoute] string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var editCart = await _customerCartService.DeleteUpdateAllCart(cartId, cancellationToken);
            if (editCart.Status == false) return BadRequest(editCart);
            return Ok(editCart);
        }
    }
}
