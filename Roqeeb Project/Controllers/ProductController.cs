using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels;
using Roqeeb_Project.View_Models.RequestModels.ProductRequestMosels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProduct(request, cancellationToken);
            if (product.Status == false) return BadRequest(product.Message);
            return Ok(product);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequestModel request, CancellationToken cancellationToken)
        {
            var newProduct = await _productService.UpdateProduct(request, cancellationToken);
            if (newProduct.Status == false) return BadRequest(newProduct.Message);
            return Ok(newProduct);
        }
        [HttpGet("GetProduct/{productName}")]
        public async Task<IActionResult> GetProduct([FromRoute] string productName, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByName(productName, cancellationToken);
            if (product.Status == false) return BadRequest(product.Message);
            return Ok(product);
        }
        [HttpGet("ViewAll")]
        public async Task<IActionResult> ViewAllProducts(CancellationToken cancellationToken)
        {
             var products = await _productService.ViewAllProducts(cancellationToken);
            if(products.Status == false) return BadRequest(products.Message);
            return Ok(products);
        }
        [HttpGet("Track/{productName}")]
        public async Task<IActionResult> TrackProduct([FromRoute] string productName, CancellationToken cancellationToken)
        {
            var productLocation = await _productService.TrackProduct(productName, cancellationToken);
            if (productLocation.Status == false) return BadRequest(productLocation.Message);
            return Ok(productLocation);
        }
    }
}
