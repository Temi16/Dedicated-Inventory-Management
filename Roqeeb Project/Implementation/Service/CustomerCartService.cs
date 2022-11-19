using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class CustomerCartService : ICustomerCartService
    {
        public readonly ICustomerCartRepository _customerCartRepository;
        public readonly IProductRepository _productRepository;
        public readonly IProductCustomerCartRepository _productCustomerCartRepository;
        public readonly IIdentityService _identityService;
        public CustomerCartService(ICustomerCartRepository customerCartRepository, IProductRepository productRepository, IProductCustomerCartRepository productCustomerCartRepository, IIdentityService identityService)
        {
            _customerCartRepository = customerCartRepository;
            _productRepository = productRepository;
            _productCustomerCartRepository = productCustomerCartRepository;
            _identityService = identityService;
        }
        public async Task<BaseResponse<CustomerCartDTO>> AddToCart(AddOrdersToCartRequestModel request, string customerCartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductByNameAsync(request.ProductName, cancellationToken);
            if (product == null) return new BaseResponse<CustomerCartDTO>
            {
                Message = "Product does not exist",
                Status = false,
            };
            var cart = await _customerCartRepository.GetAsync(sc => sc.Id == customerCartId, cancellationToken);
            List<string> products = new List<string>();
            foreach (var item in cart.ProductCustomerCarts)
            {
                products.Add(item.ProductName.ToLower());

            }
            if (products.Contains(product.ProductName.ToLower()))
            {
                return new BaseResponse<CustomerCartDTO>
                {
                    Message = "Product exists in cart",
                    Status = false
                };
            }
            if (request.ProductQuantity > product.Quantity) return new BaseResponse<CustomerCartDTO>
            {
                Message = "Requested quantity more than available quantity",
                Status = false,
            };

            var productCustomerCart = new ProductCustomerCart
            {
                CustomerCartId = cart.Id,
                ProductName = request.ProductName,
                Price = product.SellingPrice,
                ProductQuantity = request.ProductQuantity,
            };
            await _productCustomerCartRepository.CreateAsync(productCustomerCart, cancellationToken);
            var getProductCustomerCart = await _productCustomerCartRepository.GetProductCustomerCartByCartId(cart.Id, cancellationToken);
            double total = 0;
            foreach (var newProductCustomerCart in getProductCustomerCart)
            {
                var allPrice = newProductCustomerCart.Price * newProductCustomerCart.ProductQuantity;
                total += allPrice;
            }
            cart.TotalAmount = total;
            await _customerCartRepository.UpdateAsync(cart, cancellationToken);
            return new BaseResponse<CustomerCartDTO>
            {
                Data = new CustomerCartDTO
                {
                    Id = cart.Id,
                    ProductCustomerCarts = getProductCustomerCart.Select(ps => new ProductCustomerCartDTO
                    {
                        ProductName = ps.ProductName,
                        ProductQuantity = ps.ProductQuantity,
                        ProductPrice = ps.Price,
                        TotalPrice = ps.ProductQuantity * ps.Price
                    }).ToList(),
                    TotalAmount = total,
                },
                Message = "Successfully added",
                Status = true

            };
        }

        public async Task<BaseResponse<CustomerCartDTO>> CreateCart(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _customerCartRepository.GetAllAsync(ac => ac.IsActive == true && ac.UserId == userId, cancellationToken);
            if (cart.Count != 0) return new BaseResponse<CustomerCartDTO>
            {
                Message = "Exists",
                Status = false
            };
            var newCart = new CustomerCart
            {
                IsActive = true,
                UserId = userId,
            };
            await _customerCartRepository.CreateAsync(newCart, cancellationToken);
            return new BaseResponse<CustomerCartDTO>
            {
                Data = new CustomerCartDTO
                {
                    Id = newCart.Id,
                    ProductCustomerCarts = null,
                    TotalAmount = 0
                },
                Message = "Successfully Created",
                Status = true
            };
        }

        public Task<BaseResponse<CustomerCartDTO>> DeleteUpdateAllCart(string cartId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseResponse<CustomerCartDTO>> DeleteUpdateCart(string cartId, string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _customerCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<CustomerCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            var productCustomerCart = await _productCustomerCartRepository.GetAsync(psc => psc.ProductName == productName && psc.CustomerCartId == cart.Id, cancellationToken);
            await _productCustomerCartRepository.DeleteAsync(productCustomerCart, cancellationToken);
            return new BaseResponse<CustomerCartDTO>
            {

                Message = "Successfully removed",
                Status = true

            };
        }

        public async Task<BaseResponse<CustomerCartDTO>> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _customerCartRepository.GetAsync(sc => sc.IsActive == true, cancellationToken);
            if (cart == null)
            {
                return new BaseResponse<CustomerCartDTO>
                {
                    Message = "Cart not found",
                    Status = false
                };
            }

            return new BaseResponse<CustomerCartDTO>
            {
                Data = new CustomerCartDTO
                {
                    Id = cart.Id,


                },
                Message = "Successfull",
                Status = true
            };
        }

        public async Task<BaseResponse<CustomerCartDTO>> GetByStatus(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _customerCartRepository.GetAsync(sc => sc.IsActive == true && sc.UserId == userId, cancellationToken);
            if (cart == null) return new BaseResponse<CustomerCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            List<ProductCustomerCart> products = new List<ProductCustomerCart>();
            foreach (var product in cart.ProductCustomerCarts)
            {
                products.Add(product);
            }
            double totalCartPrice = 0;
            foreach (var myProduct in products)
            {
                double totalPrice = 0;
                totalPrice += myProduct.Price * myProduct.ProductQuantity;
                totalCartPrice += totalPrice;
            }
            return new BaseResponse<CustomerCartDTO>
            {
                Data = new CustomerCartDTO
                {
                    Id = cart.Id,
                    ProductCustomerCarts = products.Select(pc => new ProductCustomerCartDTO
                    {
                        ProductName = pc.ProductName,
                        ProductQuantity = pc.ProductQuantity,
                        ProductPrice = pc.Price,
                        TotalPrice = pc.ProductQuantity * pc.Price

                    }).ToList(),
                    TotalAmount = totalCartPrice

                },
                Message = "Successful",
                Status = true
            };
        }
    }
}
