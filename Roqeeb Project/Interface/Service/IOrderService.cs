using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderDTO>> Create(string cartId, string CustomerName, CancellationToken cancellationToken);
        Task<BaseResponse<IList<OrderDTO>>> GetAll(string userId, CancellationToken cancellationToken);
        Task<BaseResponse<IList<OrderDTO>>> GetByDate(string date, CancellationToken cancellationToken);
        Task<BaseResponse<IList<OrderDTO>>> GetNonApprovedOrders(CancellationToken cancellationToken);
        Task<BaseResponse<IList<OrderDTO>>> GetAllApprovedPurchase(CancellationToken cancellationToken);
        Task<BaseResponse<OrderDTO>> ApproveOrder(string orderId, CancellationToken cancellationToken);
    }
}
