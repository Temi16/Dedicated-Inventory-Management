using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IPurchaseService
    {
        Task<BaseResponse<PurchaseDTO>> Create(string cartId, string supplierId, CancellationToken cancellationToken);
        Task<BaseResponse<IList<PurchaseDTO>>> GetAll(CancellationToken cancellationToken);
        Task<BaseResponse<IList<PurchaseDTO>>> GetByDate(DateTime date, CancellationToken cancellationToken);
        Task<BaseResponse<IList<PendingPurchaseDTO>>> GetNonApprovedPurchase(CancellationToken cancellationToken);
        Task<BaseResponse<PurchaseDTO>> ApprovePurchase(string purchaseId, CancellationToken cancellationToken);

    }
}
