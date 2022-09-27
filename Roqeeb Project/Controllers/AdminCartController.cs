﻿using System.Threading;
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
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetAvailableCart(CancellationToken cancellationToken)
        {
            var cart = await _adminCartService.GetByStatus(cancellationToken);
            if (cart.Status == false) return BadRequest(cart);
            return Ok(cart);
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromForm]AddToCartRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByName(request.ProductName, cancellationToken); 
            var cart = await _adminCartService.GetByStatus(cancellationToken);
            if (cart.Status == false)
            {
                BaseResponse<AdminCartDTO> newCart = null;
                newCart = await _adminCartService.CreateCart(cancellationToken);
                var addCart = await _adminCartService.AddToCart(request, newCart.Data.Id, cancellationToken);
                return Ok(addCart);
            }
            var addToCart = await _adminCartService.AddToCart(request, cart.Data.Id, cancellationToken);
            return Ok(addToCart);

        }
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> EditAdminCart(string cartId, string productCartName, int Quantity, CancellationToken cancellationToken)
        {
            var updateCart = await _adminCartService.EditUpdateCart(cartId, productCartName, Quantity, cancellationToken);
            if (updateCart.Status == false) return BadRequest(updateCart);
            return Ok(updateCart);
        }
        [HttpDelete("DeleteProductCart")]
        public async Task<IActionResult> DeleteProductInAdminCart(string cartId, string productCartName, CancellationToken cancellationToken)
        {
            var deleteProductCart = await _adminCartService.DeleteUpdateCart(cartId, productCartName, cancellationToken);
            if (deleteProductCart.Status == false) return BadRequest(deleteProductCart);
            return Ok(deleteProductCart);
        }

    }
}
