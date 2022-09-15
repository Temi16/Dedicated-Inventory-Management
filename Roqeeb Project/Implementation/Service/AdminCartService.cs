using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class AdminCartService : IAdminCartService
    {
        private readonly IAdminCartRepository _adminCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public AdminCartService(IAdminCartRepository adminCartRepository, IProductRepository productRepository, IPurchaseRepository purchaseRepository)
        {
            _adminCartRepository = adminCartRepository;
            _productRepository = productRepository;
        }


        public async Task<BaseResponse<AdminCartDTO>> CreateCart(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var getCart = await _adminCartRepository.GetAllAsync(ac => ac.IsActive == true, cancellationToken);
            if (getCart.Count != 0) return new BaseResponse<AdminCartDTO>
            {
                Message = "Exists",
                Status = false
            };
            var cart = new AdminCart
            {
                
                IsActive = true,
                
            };

            await _adminCartRepository.CreateAsync(cart, cancellationToken);
            return new BaseResponse<AdminCartDTO>
            {
               
                Message = "Successful",
                Status = true
            };
        }
        public async Task<BaseResponse<AdminCartDTO>> AddToCart(string productId, string adminCartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _adminCartRepository.AddProductToCart(productId, adminCartId, cancellationToken);
            var cart = await _adminCartRepository.GetAsync(ac => ac.Id == adminCartId, cancellationToken);
            return new BaseResponse<AdminCartDTO>
            {
                Data = new AdminCartDTO
                {
                    Id = cart.Id,
                    products = cart.ProductAdminsCart.Select(p => p.Product).Select(pr => new ProductDTO
                    {
                        ProductName = pr.ProductName,
                    }).ToList(),
                },
                Message = "Successful",
                Status = true
                
            };
        }

        public async Task<BaseResponse<AdminCartDTO>> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _adminCartRepository.GetById(cartId, cancellationToken);
            if(cart == null)
            {
                return new BaseResponse<AdminCartDTO>
                {
                    Message = "Cart not found",
                    Status = false
                };
            }

            return new BaseResponse<AdminCartDTO>
            {
                Data = new AdminCartDTO
                {
                    Id = cart.Id,

                },
                Message = "Successfull",
                Status = true
            };
        }
        public async Task<BaseResponse<AdminCartDTO>> GetByStatus(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _adminCartRepository.GetAsync(c => c.IsActive == true, cancellationToken);
            if (cart == null) return new BaseResponse<AdminCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            return new BaseResponse<AdminCartDTO>
            {
                Data = new AdminCartDTO
                {
                    Id = cart.Id,
                },
                Message = "Successful",
                Status = true
            };
            
        }

        public async Task<BaseResponse<AdminCartDTO>> UpdateCart(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _adminCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<AdminCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            cart.IsActive = false;
            await _adminCartRepository.UpdateAsync(cart, cancellationToken);
            return new BaseResponse<AdminCartDTO>
            {
                Message = "successfully updated",
                Status = true
            };
        }
    }
}
