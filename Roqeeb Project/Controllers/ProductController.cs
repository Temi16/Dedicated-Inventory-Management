using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request, CancellationToken cancellationToken)
        {
            var files = HttpContext.Request.Form;
            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo info = new FileInfo(file.FileName);
                    string image = Guid.NewGuid().ToString() + info.Extension;
                    string path = Path.Combine(imageDirectory, image);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    request.Image = image;
                }
            }
            var product = await _productService.CreateProduct(request, cancellationToken);
            if (product.Status == false) return BadRequest(product);
            return Ok(product);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequestModel request, CancellationToken cancellationToken)
        {
            var files = HttpContext.Request.Form;
            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo info = new FileInfo(file.FileName);
                    string image = Guid.NewGuid().ToString() + info.Extension;
                    string path = Path.Combine(imageDirectory, image);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    request.Image = image;
                }
            }
            var newProduct = await _productService.UpdateProduct(request, cancellationToken);
            if (newProduct.Status == false) return BadRequest(newProduct);
            return Ok(newProduct);
        }
        [HttpGet("GetProduct/{productName}")]
        public async Task<IActionResult> GetProduct([FromRoute] string productName, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByName(productName, cancellationToken);
            if (product.Status == false) return BadRequest(product);
            return Ok(product);
        }
        [HttpGet("ViewAll")]
        public async Task<IActionResult> ViewAllProducts(CancellationToken cancellationToken)
        {
             var products = await _productService.ViewAllProducts(cancellationToken);
            if(products.Status == false) return BadRequest(products);
            return Ok(products);
        }
        [HttpGet("Track/{productName}")]
        public async Task<IActionResult> TrackProduct([FromRoute] string productName, CancellationToken cancellationToken)
        {
            var productLocation = await _productService.TrackProduct(productName, cancellationToken);
            if (productLocation.Status == false) return BadRequest(productLocation);
            return Ok(productLocation);
        }
    }
}
