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
    public class SalesCartService : ISalesCartService
    {
        public readonly ISalesCartRepository _salesCartRepository;
        public readonly IProductRepository _productRepository;
        public readonly IProductSalesCartRepository _productSalesCartRepository;
        public SalesCartService(ISalesCartRepository salesCartRepository, IProductRepository productRepository, IProductSalesCartRepository productSalesCartRepository)
        {
            _salesCartRepository = salesCartRepository;
            _productRepository = productRepository;
            _productSalesCartRepository = productSalesCartRepository;
        }
        public async Task<BaseResponse<SalesCartDTO>> AddToCart(AddSalesToCartRequestModel request, string salesCartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productRepository.GetProductByNameAsync(request.ProductName, cancellationToken);
            if (product == null) return new BaseResponse<SalesCartDTO>
            {
                Message = "Product does not exist",
                Status = false,
            };
            var cart = await _salesCartRepository.GetAsync(sc => sc.Id == salesCartId, cancellationToken);
            List<string> products = new List<string>();
            foreach (var item in cart.ProductSalesCarts)
            {
                products.Add(item.ProductName.ToLower());

            }
            if (products.Contains(product.ProductName.ToLower()))
            {
                return new BaseResponse<SalesCartDTO>
                {
                    Message = "Product exists in cart",
                    Status = false
                };
            }
            var date = DateTime.Today.ToString("dd:MM:yy");
            var month = DateTime.Now.ToString("MM");
            var stopWeek = DateTime.Now.AddDays(7);
            if (request.ProductQuantity > product.Quantity) return new BaseResponse<SalesCartDTO>
            {
                Message = "Requested quantity more than available quantity",
                Status = false,
            };

            var productSalesCart = new ProductSalesCart
            {
                SalesCartId = cart.Id,
                ProductName = request.ProductName,
                Price = product.SellingPrice,
                ProductQuantity = request.ProductQuantity,
                Date = date,
                Month = month,
                Week = stopWeek,
            };
            await _productSalesCartRepository.CreateAsync(productSalesCart, cancellationToken);
            var getProductSalesCart = await _productSalesCartRepository.GetProductSalesCartByCartId(cart.Id, cancellationToken);
            double total = 0;
            foreach(var newProductSalesCart in getProductSalesCart)
            {
                var allPrice = newProductSalesCart.Price * newProductSalesCart.ProductQuantity;
                total += allPrice;
            }
            cart.TotalAmount = total;
            await _salesCartRepository.UpdateAsync(cart, cancellationToken);
            return new BaseResponse<SalesCartDTO>
            {
                Data = new SalesCartDTO
                {
                    Id = cart.Id,
                    ProductSalesCarts = getProductSalesCart.Select(ps => new ProductSalesCartDTO
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

        public async Task<BaseResponse<SalesCartDTO>> CreateCart(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _salesCartRepository.GetAllAsync(ac => ac.IsActive == true, cancellationToken);
            var date = DateTime.Today.ToString("dd:MM:yy");
            var month = DateTime.Now.ToString("MM");
            var stopWeek = DateTime.Now.AddDays(7);
            if (cart.Count != 0) return new BaseResponse<SalesCartDTO>
            {
                Message = "Exists",
                Status = false
            };
            var newCart = new SalesCart
            {
                IsActive = true,
                Date = date,
                Month = month,
                Week = stopWeek

            };
            await _salesCartRepository.CreateAsync(newCart, cancellationToken);
            return new BaseResponse<SalesCartDTO>
            {
                Data = new SalesCartDTO
                {
                    Id = newCart.Id,
                    Date = date,
                    ProductSalesCarts = null,
                    TotalAmount = 0
                },
                Message = "Successfully Created",
                Status = true
            };
        }

        public async Task<BaseResponse<SalesCartDTO>> EditCart(string cartId, string ProductName, double sellingPrice, int quantity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _salesCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<SalesCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };

            var productSalesCart = await _productSalesCartRepository.GetAsync(psc => psc.ProductName == ProductName && psc.SalesCartId == cart.Id, cancellationToken);
            productSalesCart.ProductQuantity = quantity;
            productSalesCart.Price = sellingPrice;
            await _productSalesCartRepository.UpdateAsync(productSalesCart, cancellationToken);
            return new BaseResponse<SalesCartDTO>
            {

                Message = "Successfully Updated",
                Status = true

            };
        }
        public async Task<BaseResponse<SalesCartDTO>> DeleteUpdateCart(string cartId, string productName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _salesCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<SalesCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            var productSalesCart = await _productSalesCartRepository.GetAsync(psc => psc.ProductName == productName && psc.SalesCartId == cart.Id, cancellationToken);
            await _productSalesCartRepository.DeleteAsync(productSalesCart, cancellationToken);
            return new BaseResponse<SalesCartDTO>
            {

                Message = "Successfully removed",
                Status = true

            };
        }
        public async Task<BaseResponse<SalesCartDTO>> DeleteUpdateAllCart(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(cartId)) throw new ArgumentNullException(null);
            var cart = await _salesCartRepository.GetById(cartId, cancellationToken);
            if (cart == null) return new BaseResponse<SalesCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            var productSalesCarts = await _productSalesCartRepository.GetProductSalesCartByCartId(cart.Id, cancellationToken);
            foreach(var productCart in productSalesCarts)
            {
                await _productSalesCartRepository.DeleteAsync(productCart, cancellationToken);
            }
            return new BaseResponse<SalesCartDTO>
            {

                Message = "Successfully cleared",
                Status = true

            };
        }

        public async Task<BaseResponse<SalesCartDTO>> GetById(string cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _salesCartRepository.GetAsync(sc => sc.IsActive == true, cancellationToken);
            if (cart == null)
            {
                return new BaseResponse<SalesCartDTO>
                {
                    Message = "Cart not found",
                    Status = false
                };
            }

            return new BaseResponse<SalesCartDTO>
            {
                Data = new SalesCartDTO
                {
                    Id = cart.Id,


                },
                Message = "Successfull",
                Status = true
            };
        }

        public async Task<BaseResponse<SalesCartDTO>> GetByStatus(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cart = await _salesCartRepository.GetAsync(sc => sc.IsActive == true, cancellationToken);
            if (cart == null) return new BaseResponse<SalesCartDTO>
            {
                Message = "Does not exist",
                Status = false
            };
            List<ProductSalesCart> products = new List<ProductSalesCart>();
            foreach (var product in cart.ProductSalesCarts)
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
            return new BaseResponse<SalesCartDTO>
            {
                Data = new SalesCartDTO
                {
                    Id = cart.Id,
                    ProductSalesCarts = products.Select(pc => new ProductSalesCartDTO
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
