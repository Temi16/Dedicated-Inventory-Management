using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerCartRepository _customerCartRepository;
        private readonly IProductCustomerCartRepository _productCustomerCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRoleStore<User> _userRepository;
        private readonly IIdentityService _identityService;
        public OrderService(IOrderRepository orderRepository, ICustomerCartRepository customerCartRepository, IProductCustomerCartRepository productCustomerCartRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IUserRoleStore<User> userRepository, IIdentityService identityService)
        {
            _productCustomerCartRepository = productCustomerCartRepository;
            _customerCartRepository = customerCartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _identityService = identityService;
        }
        public async Task<BaseResponse<OrderDTO>> Create(string cartId, string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _orderRepository.GetAsync(s => s.CustomerCartId == cartId && s.UserId == userId, cancellationToken);
            //var getCustomer = await _customerRepository.GetCustomerAsync(c => c.User.Username.ToLower() == customerName.ToLower(), cancellationToken);
            //if (getCustomer == null) return new BaseResponse<OrderDTO>
            //{
            //    Message = "Username does not exist",
            //    Status = false
            //};
            var cart = await _customerCartRepository.GetById(cartId, cancellationToken);
            if (order != null) return new BaseResponse<OrderDTO>
            {
                Message = "Exists",
                Status = false
            };
            if (cart == null) return new BaseResponse<OrderDTO>
            {
                Message = "Cart does not exist",
                Status = false
            };
            var newOrder = new Order
            {
                UserId = userId,
                IsDeleted = false,
                ReferenceNo = $"SL-{Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 5)}",
                CreatedOn = DateTime.UtcNow,
                CustomerCartId = cart.Id,

            };
            await _orderRepository.AddAsync(newOrder, cancellationToken);
            cart.IsActive = false;
            await _customerCartRepository.UpdateAsync(cart, cancellationToken);
            foreach (var product in cart.ProductCustomerCarts)
            {
                var realProduct = await _productRepository.GetProductByNameAsync(product.ProductName, cancellationToken);
                realProduct.Quantity = realProduct.Quantity - product.ProductQuantity;
                realProduct.LastModifiedOn = DateTime.UtcNow;
                await _productRepository.UpdateProduct(realProduct, cancellationToken);

            }
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            return new BaseResponse<OrderDTO>
            {
                Data = new OrderDTO
                {
                    Id = newOrder.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = newOrder.CustomerCartId,
                        ProductCustomerCarts = newOrder.CustomerCart.ProductCustomerCarts.Select(pr => new ProductCustomerCartDTO
                        {

                            ProductName = pr.ProductName,
                            ProductQuantity = pr.ProductQuantity,
                            ProductPrice = pr.Price,
                            TotalPrice = pr.Price * pr.ProductQuantity
                        }).ToList(),
                        TotalAmount = newOrder.CustomerCart.TotalAmount,
                    },
                    CustomerName = user.Username,
                    ReferenceNo = newOrder.ReferenceNo
                },
                Message = "Successful",
                Status = true
            };

        }

        public async Task<BaseResponse<IList<OrderDTO>>> GetAll(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var order = await _orderRepository.GetAllAsync(or => or.UserId == userId, cancellationToken);
            if (order.Count == 0) return new BaseResponse<IList<OrderDTO>>
            {
                Message = "Failed",
                Status = false
            };

            return new BaseResponse<IList<OrderDTO>>
            {
                Data = order.Select(s => new OrderDTO
                {

                    Id = s.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = s.CustomerCartId,
                        ProductCustomerCarts = s.CustomerCart.ProductCustomerCarts.Select(ps => new ProductCustomerCartDTO
                        {

                            ProductName = ps.ProductName,
                            ProductQuantity = ps.ProductQuantity,
                            ProductPrice = ps.Price,
                            TotalPrice = ps.Price * ps.ProductQuantity
                        }).ToList(),
                        TotalAmount = s.CustomerCart.TotalAmount,
                    },
                    CustomerName = s.User.Username,
                    ReferenceNo = s.ReferenceNo,
                    Date = s.CreatedOn.ToString(),
                    TotalCost = s.CustomerCart.TotalAmount

                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<OrderDTO>>> GetByDate(string date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var order = await _orderRepository.GetAllAsync(s => s.CreatedOn.ToString().Contains(date), cancellationToken);

            if (order.Count == 0)
            {
                return new BaseResponse<IList<OrderDTO>>
                {
                    Status = false,
                    Message = "No sales made on this date",

                };
            }

            return new BaseResponse<IList<OrderDTO>>
            {
                Data = order.Select(s => new OrderDTO
                {
                    Id = s.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = s.CustomerCartId,
                        ProductCustomerCarts = s.CustomerCart.ProductCustomerCarts.Select(ps => new ProductCustomerCartDTO
                        {

                            ProductName = ps.ProductName,
                            ProductQuantity = ps.ProductQuantity,
                            ProductPrice = ps.Price,
                            TotalPrice = ps.Price * ps.ProductQuantity
                        }).ToList(),
                        TotalAmount = s.CustomerCart.TotalAmount,
                    },
                    CustomerName = s.User.Username,
                    ReferenceNo = s.ReferenceNo,
                    Date = s.CreatedOn.ToString(),
                    TotalCost = s.CustomerCart.TotalAmount,

                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }
        public async Task<BaseResponse<IList<OrderDTO>>> GetNonApprovedOrders(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var unapprovedOrders = await _orderRepository.GetAllAsync(or => or.IsApproved == false, cancellationToken);
            if (unapprovedOrders.Count == 0) return new BaseResponse<IList<OrderDTO>>
            {
                Message = "No Avaiable Pending order",
                Status = false,
            };
            return new BaseResponse<IList<OrderDTO>>
            {
                Data = unapprovedOrders.Select(pp => new OrderDTO
                {
                    Id = pp.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = pp.CustomerCartId,
                        ProductCustomerCarts = pp.CustomerCart.ProductCustomerCarts.Select(pc => new ProductCustomerCartDTO
                        {
                            ProductName = pc.ProductName,
                            ProductQuantity = pc.ProductQuantity,
                            ProductPrice = pc.Price,
                            TotalPrice = pc.Price * pc.ProductQuantity,
                        }).ToList(),
                        TotalAmount = pp.CustomerCart.TotalAmount
                    },
                    CustomerName = pp.User.Username,
                    ReferenceNo = pp.ReferenceNo,
                    Date = pp.CreatedOn.ToString(),
                    TotalCost = pp.CustomerCart.TotalAmount

                }).ToList(),
                Message = "Successfull",
                Status = true
            };

        }
        public async Task<BaseResponse<IList<OrderDTO>>> GetAllApprovedPurchase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var approvedOrder = await _orderRepository.GetAllAsync(p => p.IsApproved == true, cancellationToken);
            if (approvedOrder.Count == 0) return new BaseResponse<IList<OrderDTO>>
            {
                Message = "No Approved Purchase",
                Status = false,
            };
            return new BaseResponse<IList<OrderDTO>>
            {
                Data = approvedOrder.Select(pp => new OrderDTO
                {
                    Id = pp.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = pp.CustomerCartId,
                        ProductCustomerCarts = pp.CustomerCart.ProductCustomerCarts.Select(pc => new ProductCustomerCartDTO
                        {
                            ProductName = pc.ProductName,
                            ProductQuantity = pc.ProductQuantity,
                            ProductPrice = pc.Price,
                            TotalPrice = pc.Price * pc.ProductQuantity,
                        }).ToList(),
                        TotalAmount = pp.CustomerCart.TotalAmount

                    },
                    CustomerName = pp.User.Username,
                    ReferenceNo = pp.ReferenceNo,
                    Date = pp.CreatedOn.ToString(),
                    TotalCost = pp.CustomerCart.TotalAmount

                }).ToList(),
                Message = "Successfull",
                Status = true
            };
        }
        public async Task<BaseResponse<OrderDTO>> ApproveOrder(string orderId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(orderId)) throw new ArgumentNullException(nameof(orderId));
            var order = await _orderRepository.GetAsync(p => p.Id == orderId, cancellationToken);
            if (order == null) return new BaseResponse<OrderDTO>
            {
                Message = "Not Found",
                Status = false
            };
            order.IsApproved = true;
            //foreach (var product in order.CustomerCart.ProductCustomerCarts)
            //{
            //    var realProduct = await _productRepository.GetProductByNameAsync(product.ProductName, cancellationToken);
            //    if (realProduct != null)
            //    {
            //        realProduct.Quantity = realProduct.Quantity - product.ProductQuantity;
            //        await _productRepository.UpdateProduct(realProduct, cancellationToken);
            //    }
            //    else
            //    {
            //        var newProduct = new Product
            //        {
            //            ProductName = product.ProductName,
            //            ProductDescription = "",
            //            CostPrice = 0,
            //            SellingPrice = 0,
            //            Quantity = product.ProductQuantity,
            //            CreatedBy = _identityService.GetUserIdentity(),
            //            CreatedOn = DateTime.UtcNow,
            //            IsAvalaible = true,
            //            IsDeleted = false,
            //        };
            //        await _productRepository.CreateProductAsync(newProduct, cancellationToken);
            //    }
                
            //}
            await _orderRepository.UpdateAsync(order, cancellationToken);
            return new BaseResponse<OrderDTO>
            {
                Data = new OrderDTO
                {
                    Id = order.Id,
                    Cart = new CustomerCartDTO
                    {
                        Id = order.CustomerCartId,
                        ProductCustomerCarts = order.CustomerCart.ProductCustomerCarts.Select(pr => new ProductCustomerCartDTO
                        {

                            ProductName = pr.ProductName,
                            ProductQuantity = pr.ProductQuantity,
                            ProductPrice = pr.Price,
                            TotalPrice = pr.Price * pr.ProductQuantity,
                        }).ToList(),
                        TotalAmount = order.CustomerCart.TotalAmount
                    },
                    CustomerName = order.User.Username,
                    ReferenceNo = order.ReferenceNo,
                    Date = order.CreatedOn.ToString(),
                    TotalCost = order.CustomerCart.TotalAmount
                },
                Message = "Approved Successfully",
                Status = true
            };
        }
        
    }
}
