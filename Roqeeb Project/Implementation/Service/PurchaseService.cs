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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAdminCartRepository _adminCartRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository, IAdminCartRepository adminCartRepository)
        {
            _purchaseRepository = purchaseRepository;
            _adminCartRepository = adminCartRepository;
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
            await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
            return new BaseResponse<PurchaseDTO>
            {
                Data = new PurchaseDTO
                {
                    CartId = purchase.AdminCartId,
                    Id = purchase.Id,
                },
                Message = "Approved Successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<PurchaseDTO>> Create(string cartId, CancellationToken cancellationToken)
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
                IsApproved = false
            };
            await _purchaseRepository.AddAsync(newPurchase, cancellationToken);
            cart.IsActive = false;
            await _adminCartRepository.UpdateAsync(cart, cancellationToken);
            return new BaseResponse<PurchaseDTO>
            {
                Data = new PurchaseDTO
                {
                    Id = newPurchase.Id,
                    CartId = cart.Id,
                    
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
                    CartId = p.Id

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
                    CartId = p.Id

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
                    cart = new AdminCartDTO
                    {
                        Id = pp.AdminCartId,
                        Products = pp.AdminCart.productCarts.Select(pc => new ProductCartDTO
                        {
                            ProductName = pc.ProductName,
                            Quantity = pc.Quantity
                        }).ToList(),
                        TotalAmount = pp.AdminCart.TotalAmount

                    }
                }).ToList(),
                Message = "Successfull",
                Status = true
            };
          
        }
    }
}
