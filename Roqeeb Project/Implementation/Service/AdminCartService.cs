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
        private readonly IProductCartRepository _productCartRepository;
        public AdminCartService(IAdminCartRepository adminCartRepository, IProductRepository productRepository, IProductCartRepository productCartRepository)
        {
            _adminCartRepository = adminCartRepository;
            _productRepository = productRepository;
            _productCartRepository = productCartRepository;
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
                Data = new AdminCartDTO
                {
                    Id = cart.Id,
                    TotalAmount = 0,
                    Products = null
                },
                Message = "Successful",
                Status = true
            };
        }
        public async Task<BaseResponse<AdminCartDTO>> AddToCart(AddToCartRequestModel request, string adminCartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductByNameAsync(request.ProductName, cancellationToken);
           
            var cart = await _adminCartRepository.GetAsync(ac => ac.Id == adminCartId, cancellationToken);
            if(product != null)
            {
                List<string> products = new List<string>();
                foreach (var item in cart.productCarts)
                {
                    products.Add(item.ProductName.ToLower());

                }
                if (products.Contains(product.ProductName.ToLower()))
                {
                    return new BaseResponse<AdminCartDTO>
                    {
                        Message = "Product exists in cart",
                        Status = false
                    };
                }
                await _adminCartRepository.AddProductToCart(product.Id, adminCartId, cancellationToken);
            }
           
            var productCart = new ProductCart
            {
                ProductName = request.ProductName,
                Quantity = request.ProductQuantity,
                Price = request.ProductPrice,
                AdminCartId = cart.Id,
                CreatedOn = DateTime.UtcNow,
            };
            await _productCartRepository.CreateAsync(productCart, cancellationToken);
            var productCarts = await _productCartRepository.GetProductCartByCartId(cart.Id, cancellationToken);
            double totalPrice = 0;
            foreach(var myPrice in productCarts)
            {
                totalPrice += myPrice.Price;
            }
            return new BaseResponse<AdminCartDTO>
            {
                Data = new AdminCartDTO
                {
                    Id = cart.Id,
                    Products = productCarts.Select(pc => new ProductCartDTO
                    {
                        ProductName = pc.ProductName,
                        Quantity = pc.Quantity,
                        Price = pc.Price
                    }).ToList(),
                    TotalAmount = totalPrice
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
            List<ProductCart> products = new List<ProductCart>();
            foreach(var product in cart.productCarts)
            {
                products.Add(product);
            }
            double totalPrice = 0;
            foreach(var myProduct in products)
            {
                totalPrice += myProduct.Price;
            }
            return new BaseResponse<AdminCartDTO>
            {
                Data = new AdminCartDTO
                {
                    Id = cart.Id,
                    Products = products.Select(pc => new ProductCartDTO
                    {
                        ProductName = pc.ProductName,
                        Quantity = pc.Quantity
                    }).ToList(),
                    TotalAmount = totalPrice

                },
                Message = "Successful",
                Status = true
            };
            
        }

        public async Task<BaseResponse<AdminCartDTO>> EditUpdateCart(string cartId, string productCartName, int Quantity, double price, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _adminCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<AdminCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };

            var productCart = await _productCartRepository.GetAsync(pc => pc.ProductName == productCartName && pc.AdminCartId == cart.Id, cancellationToken);
            productCart.Quantity = Quantity;
            productCart.Price = price;
            await _productCartRepository.UpdateAsync(productCart, cancellationToken);
            return new BaseResponse<AdminCartDTO>
            {
               
                Message = "Successfully Updated",
                Status = true

            };
        }
        public async Task<BaseResponse<AdminCartDTO>> DeleteUpdateCart(string cartId, string productCartName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _adminCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<AdminCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            var productCart = await _productCartRepository.GetAsync(pc => pc.ProductName == productCartName && pc.AdminCartId == cart.Id, cancellationToken);
            await _productCartRepository.DeleteAsync(productCart, cancellationToken);
            return new BaseResponse<AdminCartDTO>
            {

                Message = "Successfully Updated",
                Status = true
            };
        }

        public async Task<BaseResponse<bool>> CheckProductExistBeforeAddingToCart(string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductByNameAsync(productName, cancellationToken);
            if (product == null) return new BaseResponse<bool>
            {
                Status = false
            };
            return new BaseResponse<bool>
            {
                Status = true
            };

            
        }
    }
}
