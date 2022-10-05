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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAdminCartRepository _adminCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIdentityService _identityService;
        public PurchaseService(IPurchaseRepository purchaseRepository, IAdminCartRepository adminCartRepository, IProductRepository productRepository, IIdentityService identityService)
        {
            _purchaseRepository = purchaseRepository;
            _adminCartRepository = adminCartRepository;
            _productRepository = productRepository;
            _identityService = identityService;
        }

        public async Task<BaseResponse<PurchaseDTO>> ApprovePurchase(string purchaseId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(purchaseId)) throw new ArgumentNullException(nameof(purchaseId));
            var purchase = await _purchaseRepository.GetAsync(p => p.Id == purchaseId, cancellationToken);
            if (purchase == null) return new BaseResponse<PurchaseDTO>
            {
                Message = "Not Found",
                Status = false
            };
            purchase.IsApproved = true;
            foreach (var product in purchase.AdminCart.productCarts)
            {
                var realProduct = await _productRepository.GetProductByNameAsync(product.ProductName, cancellationToken);
                if(realProduct != null)
                {
                    realProduct.Quantity = product.Quantity + realProduct.Quantity;
                    realProduct.CostPrice = product.Price;
                    await _productRepository.UpdateProduct(realProduct, cancellationToken);
                }
                else
                {
                    var newProduct = new Product
                    {
                        ProductName = product.ProductName,
                        ProductDescription = "",
                        CostPrice = product.Price,
                        SellingPrice = 0,
                        Quantity = product.Quantity,
                        CreatedBy = _identityService.GetUserIdentity(),
                        CreatedOn = DateTime.UtcNow,
                        IsAvalaible = true,
                        IsDeleted = false,
                    };
                    await _productRepository.CreateProductAsync(newProduct, cancellationToken);
                }
              
            }
            await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
            return new BaseResponse<PurchaseDTO>
            {
                Data = new PurchaseDTO
                {
                    Id = purchase.Id,
                    cart = new AdminCartDTO
                    {
                        Id = purchase.AdminCartId,
                        Products = purchase.AdminCart.productCarts.Select(pr => new ProductCartDTO
                        {

                            ProductName = pr.ProductName,
                            Quantity = pr.Quantity
                        }).ToList(),
                        TotalAmount = purchase.AdminCart.TotalAmount
                    },
                    //SupplierName = purchase.Supplier.SupplierName,
                    ReferenceNo = purchase.ReferenceNo
                },
                Message = "Approved Successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<PurchaseDTO>> Create(string cartId, string supplierId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _purchaseRepository.GetAsync(p => p.AdminCartId == cartId, cancellationToken);
            var cart = await _adminCartRepository.GetById(cartId, cancellationToken);
            if (purchase != null) return new BaseResponse<PurchaseDTO>
            {
                Message = "Exists",
                Status = false
            };
            if (cart == null) return new BaseResponse<PurchaseDTO>
            {
                Message = "Cart does not exist",
                Status = false
            };
            var newPurchase = new Purchase
            {
                AdminCartId = cart.Id,
                IsDeleted = false,
                IsApproved = false,
                SupplierId = supplierId,
                ReferenceNo = $"RC-{Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 5)}",
                CreatedOn = DateTime.UtcNow


            };
            await _purchaseRepository.AddAsync(newPurchase, cancellationToken);
            cart.IsActive = false;
            await _adminCartRepository.UpdateAsync(cart, cancellationToken);
            return new BaseResponse<PurchaseDTO>
            {
                Data = new PurchaseDTO
                {
                    Id = newPurchase.Id,
                    cart = new AdminCartDTO
                    {
                        Id = newPurchase.AdminCartId,
                        Products = newPurchase.AdminCart.productCarts.Select(pr => new ProductCartDTO
                        {

                            ProductName = pr.ProductName,
                            Quantity = pr.Quantity
                        }).ToList(),
                        TotalAmount = newPurchase.AdminCart.TotalAmount,
                    },
                    //SupplierName = newPurchase.Supplier.SupplierName,
                    ReferenceNo = newPurchase.ReferenceNo
                },
                Message = "Successful",
                Status = true
            };

        }

        public async Task<BaseResponse<IList<PurchaseDTO>>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchases = await _purchaseRepository.GetAll(cancellationToken);
            if (purchases.Count == 0) return new BaseResponse<IList<PurchaseDTO>>
            {
                Message = "Failed",
                Status = false
            };
            return new BaseResponse<IList<PurchaseDTO>>
            {
                Data = purchases.Select(p => new PurchaseDTO
                {
                    Id = p.Id,
                    cart = new AdminCartDTO
                    {
                        Id = p.AdminCartId,
                        Products = p.AdminCart.productCarts.Select(pr => new ProductCartDTO
                        {
                           
                            ProductName = pr.ProductName,
                            Quantity = pr.Quantity
                        }).ToList(),
                        TotalAmount = p.AdminCart.TotalAmount,
                    },
                    SupplierName = p.Supplier.SupplierName,
                    ReferenceNo = p.ReferenceNo

                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<PurchaseDTO>>> GetByDate(DateTime date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            date = date.ToUniversalTime();
            var purchases = await _purchaseRepository.GetAllAsync(p => p.CreatedOn == date, cancellationToken);
            if (purchases.Count == 0) return new BaseResponse<IList<PurchaseDTO>>
            {
                Message = "Failed",
                Status = false
            };
            return new BaseResponse<IList<PurchaseDTO>>
            {
                Data = purchases.Select(p => new PurchaseDTO
                {
                    Id = p.Id,
                    cart = new AdminCartDTO
                    {
                        Id = p.AdminCartId,
                        Products = p.AdminCart.productCarts.Select(pr => new ProductCartDTO
                        {

                            ProductName = pr.ProductName,
                            Quantity = pr.Quantity
                        }).ToList(),
                        TotalAmount = p.AdminCart.TotalAmount
                    },
                    SupplierName = p.Supplier.SupplierName,
                    ReferenceNo = p.ReferenceNo

                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<PendingPurchaseDTO>>> GetNonApprovedPurchase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var unapprovedPurchase = await _purchaseRepository.GetAllAsync(p => p.IsApproved == false, cancellationToken);
            if (unapprovedPurchase.Count == 0) return new BaseResponse<IList<PendingPurchaseDTO>>
            {
                Message = "No Avaiable Pending Purchase",
                Status = false,
            };
            return new BaseResponse<IList<PendingPurchaseDTO>>
            {
                Data = unapprovedPurchase.Select(pp => new PendingPurchaseDTO
                {
                    Id = pp.Id,
                    cart = new AdminCartDTO
                    {
                        Id = pp.AdminCartId,
                        Products = pp.AdminCart.productCarts.Select(pc => new ProductCartDTO
                        {
                            ProductName = pc.ProductName,
                            Quantity = pc.Quantity
                        }).ToList(),
                        TotalAmount = pp.AdminCart.TotalAmount

                    },
                   // SupplierName = pp.Supplier.SupplierName,
                    ReferenceNo = pp.ReferenceNo,
                    DateCreated = pp.CreatedOn.ToString()
                    
                }).ToList(),
                Message = "Successfull",
                Status = true
            };
          
        }
    }
}
