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
    public class SalesCartController : ControllerBase
    {
        private readonly ISalesCartService _salesCartService;
        private readonly IProductService _productService;
        public SalesCartController(ISalesCartService salesCartService, IProductService productService)
        {
            _salesCartService = salesCartService;
            _productService = productService;
        }
        [HttpGet("GetSalesCart")]
        public async Task<IActionResult> GetAvailableSalesCart(CancellationToken cancellationToken)
        {
            var cart = await _salesCartService.GetByStatus(cancellationToken);
            if (cart.Status == false) return BadRequest(cart);
            return Ok(cart);
        }
        [HttpPost("AddToSalesCart")]
        public async Task<IActionResult> AddToCart([FromForm] AddSalesToCartRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByName(request.ProductName, cancellationToken);
            var cart = await _salesCartService.GetByStatus(cancellationToken);
            if (cart.Status == false)
            {
                BaseResponse<SalesCartDTO> newCart = null;
                newCart = await _salesCartService.CreateCart(cancellationToken);
                var addCart = await _salesCartService.AddToCart(request, newCart.Data.Id, cancellationToken);
                return Ok(addCart);
            }
            var addToCart = await _salesCartService.AddToCart(request, cart.Data.Id, cancellationToken);
            return Ok(addToCart);

        }
        [HttpPut("EditCart/{cartId}/{ProductName}/{sellingPrice}/{quantity}")]
        public async Task<IActionResult> EditCart([FromRoute]string cartId, [FromRoute]string ProductName, [FromRoute]double sellingPrice, [FromRoute]int quantity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var editCart = await _salesCartService.EditCart(cartId, ProductName, sellingPrice, quantity, cancellationToken);
            if (editCart.Status == false) return BadRequest(editCart);
            return Ok(editCart);
        }
        [HttpDelete("DeleteProductCart/{cartId}/{ProductName}")]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] string cartId, [FromRoute] string ProductName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var editCart = await _salesCartService.DeleteUpdateCart(cartId, ProductName, cancellationToken);
            if (editCart.Status == false) return BadRequest(editCart);
            return Ok(editCart);
        }
        [HttpDelete("DeleteAllProductCart/{cartId}")]
        public async Task<IActionResult> RemoveAllProductFromCart([FromRoute] string cartId,  CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var editCart = await _salesCartService.DeleteUpdateAllCart(cartId, cancellationToken);
            if (editCart.Status == false) return BadRequest(editCart);
            return Ok(editCart);
        }
    }
}
