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
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesCartRepository _salesCartRepository;
        private readonly IProductSalesCartRepository _productSalesCartRepository;
        private readonly IProductRepository _productRepository;
        public SalesService(ISalesRepository salesRepository, ISalesCartRepository salesCartRepository, IProductSalesCartRepository productSalesCartRepository, IProductRepository productRepository)
        {
            _productSalesCartRepository = productSalesCartRepository;
            _salesCartRepository = salesCartRepository;
            _salesRepository = salesRepository;
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<ViewSalesDTO>> ViewSalesThisWeek(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Dictionary<string, int> products = new Dictionary<string, int>();
            var date = DateTime.Now;

            var productNames = await _productRepository.GetAllProductNames(cancellationToken);
            double totalCostPrice = 0;
            foreach (var productName in productNames)
            {
                var getProductCarts = await _productSalesCartRepository.GetAllAsync(pc => pc.ProductName.ToLower() == productName.ToLower() && pc.Week > date, cancellationToken);
                if (getProductCarts.Count != 0)
                {
                    var name = "";
                    var totalQuantity = 0;
                    foreach (var productCart in getProductCarts)
                    {
                        totalQuantity += productCart.ProductQuantity;
                        name = productCart.ProductName;
                        var product = await _productRepository.GetProductByNameAsync(productName, cancellationToken);
                        totalCostPrice += product.CostPrice * totalQuantity;
                    }
                    products.Add(name, totalQuantity);

                   
                }

            }
            var salesCart = await _salesCartRepository.GetAllCartAsync(cancellationToken);
            double totalSellingPrice = 0;
            foreach (var cart in salesCart)
            {
                totalSellingPrice += cart.TotalAmount;
            }

            return new BaseResponse<ViewSalesDTO>
            {
                Data = new ViewSalesDTO
                {
                    Products = products,
                    TotalSellingPrice = totalSellingPrice,
                    TotalCostPrice = totalCostPrice,
                    Profit = totalCostPrice - totalSellingPrice
                },
                Message = "Successful",
                Status = true

            };
        }
        public async Task<BaseResponse<ViewSalesDTO>> ViewSalesThisMonth(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Dictionary<string, int> products = new Dictionary<string, int>();
            var month = DateTime.Now.ToString("MM");
           
            var productNames = await _productRepository.GetAllProductNames(cancellationToken);
            double totalCostPrice = 0;
            foreach (var productName in productNames)
            {
                var getProductCarts = await _productSalesCartRepository.GetAllAsync(pc => pc.ProductName.ToLower() == productName.ToLower() && pc.Month == month, cancellationToken);
                if (getProductCarts.Count != 0)
                {
                    var name = "";
                    var totalQuantity = 0;
                    foreach (var productCart in getProductCarts)
                    {
                        totalQuantity += productCart.ProductQuantity;
                        name = productCart.ProductName;
                        var product = await _productRepository.GetProductByNameAsync(productName, cancellationToken);
                        totalCostPrice += product.CostPrice * totalQuantity;
                    }
                    products.Add(name, totalQuantity);

                    
                }

            }
            var salesCart = await _salesCartRepository.GetAllCartAsync(cancellationToken);
            double totalSellingPrice = 0;
            foreach (var cart in salesCart)
            {
                totalSellingPrice += cart.TotalAmount;
            }

            return new BaseResponse<ViewSalesDTO>
            {
                Data = new ViewSalesDTO
                {
                    Products = products,
                    TotalSellingPrice = totalSellingPrice,
                    TotalCostPrice = totalCostPrice,
                    Profit = totalCostPrice - totalSellingPrice
                },
                Message = "Successful",
                Status = true

            };
        }
        public async Task<BaseResponse<ViewSalesDTO>> ViewSalesToday(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Dictionary<string, int> products = new Dictionary<string, int>();
            //var getAllSales = await _productSalesCartRepository.GetAll(cancellationToken);
            var date = DateTime.Today.ToString("dd:MM:yy");
            var productNames = await _productRepository.GetAllProductNames(cancellationToken);
            double totalCostPrice = 0;
            foreach (var productName in productNames)
            {
                var getProductCarts = await _productSalesCartRepository.GetAllAsync(pc => pc.ProductName.ToLower() == productName.ToLower() && pc.Date == date, cancellationToken);
                if(getProductCarts.Count != 0)
                {
                    var name = "";
                    var totalQuantity = 0;
                    foreach (var productCart in getProductCarts)
                    {
                        totalQuantity += productCart.ProductQuantity;
                        name = productCart.ProductName;
                        var product = await _productRepository.GetProductByNameAsync(productName, cancellationToken);
                        totalCostPrice += product.CostPrice * totalQuantity;
                    }
                    products.Add(name, totalQuantity);

                   
                    
                }
               
            }
            var salesCart = await _salesCartRepository.GetAllCartAsync(cancellationToken);
            double totalSellingPrice = 0;
            foreach(var cart in salesCart)
            {
                totalSellingPrice += cart.TotalAmount;
            }

            return new BaseResponse<ViewSalesDTO>
            {
                Data = new ViewSalesDTO
                {
                    Products = products,
                    TotalSellingPrice = totalSellingPrice,
                    TotalCostPrice = totalCostPrice,
                    Profit = totalCostPrice - totalSellingPrice
                },
                Message = "Successful",
                Status = true
                
            };
        }
        public async Task<BaseResponse<SalesDTO>> Create(string cartId, string customerName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _salesRepository.GetAsync(s => s.SalesCartId == cartId, cancellationToken);
            var cart = await _salesCartRepository.GetById(cartId, cancellationToken);
            if (sales != null) return new BaseResponse<SalesDTO>
            {
                Message = "Exists",
                Status = false
            };
            if (cart == null) return new BaseResponse<SalesDTO>
            {
                Message = "Cart does not exist",
                Status = false
            };
            var newSales = new Sales
            {
                SalesCartId = cart.Id,
                IsDeleted = false,
                ReferenceNo = $"SL-{Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 5)}",
                CreatedOn = DateTime.UtcNow,
                CustomerName = customerName ?? "",
            };
            await _salesRepository.AddAsync(newSales, cancellationToken);
            cart.IsActive = false;
            await _salesCartRepository.UpdateAsync(cart, cancellationToken);
            foreach(var product in cart.ProductSalesCarts)
            {
                var realProduct = await _productRepository.GetProductByNameAsync(product.ProductName, cancellationToken);
                realProduct.Quantity = realProduct.Quantity - product.ProductQuantity;
                realProduct.LastModifiedOn = DateTime.UtcNow;
                await _productRepository.UpdateProduct(realProduct, cancellationToken);
                
            }
            return new BaseResponse<SalesDTO>
            {
                Data = new SalesDTO
                {
                    Id = newSales.Id,    
                    Cart = new SalesCartDTO
                    {
                        Id = newSales.SalesCartId,
                        ProductSalesCarts = newSales.SalesCart.ProductSalesCarts.Select(pr => new ProductSalesCartDTO
                        {

                            ProductName = pr.ProductName,
                            ProductQuantity = pr.ProductQuantity,
                            ProductPrice = pr.Price,
                            TotalPrice = pr.Price * pr.ProductQuantity
                        }).ToList(),
                        TotalAmount = newSales.SalesCart.TotalAmount,
                    },
                    CustomerName = newSales.CustomerName,
                    ReferenceNo = newSales.ReferenceNo
                },
                Message = "Successful",
                Status = true
            };

        }

        public async Task<BaseResponse<IList<SalesDTO>>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _salesRepository.GetAll(cancellationToken);
            if (sales.Count == 0) return new BaseResponse<IList<SalesDTO>>
            {
                Message = "Failed",
                Status = false
            };
            return new BaseResponse<IList<SalesDTO>>
            {
                Data = sales.Select(s => new SalesDTO
                {
                    Id = s.Id,
                    Cart = new SalesCartDTO
                    {
                        Id = s.SalesCartId,
                        ProductSalesCarts = s.SalesCart.ProductSalesCarts.Select(ps => new ProductSalesCartDTO
                        {

                            ProductName = ps.ProductName,
                            ProductQuantity = ps.ProductQuantity,
                            ProductPrice = ps.Price,
                            TotalPrice = ps.Price * ps.ProductQuantity
                        }).ToList(),
                        TotalAmount = s.SalesCart.TotalAmount,
                    },
                    CustomerName = s.CustomerName,
                    ReferenceNo = s.ReferenceNo

                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<SalesDTO>>> GetByDate(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            string newDate = date.ToString("yyyy-MM-dd");
            var sales = await _salesRepository.GetAllAsync(s => s.CreatedOn.ToString().Contains(newDate), cancellationToken);
            if (sales.Count == 0) return new BaseResponse<IList<SalesDTO>>
            {
                Message = "No sales made on this date",
                Status = false
            };
            return new BaseResponse<IList<SalesDTO>>
            {
                Data = sales.Select(s => new SalesDTO
                {
                    Id = s.Id,
                    Cart = new SalesCartDTO
                    {
                        Id = s.SalesCartId,
                        ProductSalesCarts = s.SalesCart.ProductSalesCarts.Select(ps => new ProductSalesCartDTO
                        {

                            ProductName = ps.ProductName,
                            ProductQuantity = ps.ProductQuantity,
                            ProductPrice = ps.Price,
                            TotalPrice = ps.Price * ps.ProductQuantity
                        }).ToList(),
                        TotalAmount = s.SalesCart.TotalAmount,
                    },
                    CustomerName = s.CustomerName,
                    ReferenceNo = s.ReferenceNo

                }).ToList(),
                Message = "Successful",
                Status = true
            };

        }
    }
}
