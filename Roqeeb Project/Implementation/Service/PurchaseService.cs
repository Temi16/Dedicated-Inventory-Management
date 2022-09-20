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

        public async Task<BaseResponse<PurchaseDTO>> Create(CreatePurchaseRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _purchaseRepository.GetAsync(p => p.AdminCartId == request.CartId, cancellationToken);
            var cart = await _adminCartRepository.GetById(request.CartId, cancellationToken);
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
                IsDeleted = false
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
    }
}
