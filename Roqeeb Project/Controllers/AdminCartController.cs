using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCartController : Controller
    {
        private readonly IAdminCartService _adminCartService;
        private readonly IProductService _productService;
        public AdminCartController(IAdminCartService adminCartService, IProductService productService)
        {
            _adminCartService = adminCartService;
            _productService = productService;
        }
        [HttpPost("CreateAdminCart")]
        public async Task<IActionResult> CreateAdminCart(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _adminCartService.CreateCart(cancellationToken);
            if (cart.Status == false) return BadRequest(cart.Message);
            return Ok(cart);
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromForm]string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByName(productName, cancellationToken); 
            var cart = await _adminCartService.GetByStatus(cancellationToken);
            BaseResponse<AdminCartDTO> newCart = null;
            if (cart.Status == false)
            {
                newCart = await _adminCartService.CreateCart(cancellationToken);
                var addCart = await _adminCartService.AddToCart(product.Data.Id, newCart.Data.Id, cancellationToken);
                return Ok(addCart);
            }
            var addToCart = await _adminCartService.AddToCart(product.Data.Id, cart.Data.Id, cancellationToken);
            return Ok(addToCart);

        }
    }
}
