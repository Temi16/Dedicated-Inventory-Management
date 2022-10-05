using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IAdminCartService
    {
        Task<BaseResponse<AdminCartDTO>> CreateCart(CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> GetById(string cartId, CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> GetByStatus(CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> AddToCart(AddToCartRequestModel request, string adminCartId, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> CheckProductExistBeforeAddingToCart(string productName, CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> EditUpdateCart(string productId, string cartId, int Quantity, double price, CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> DeleteUpdateCart(string cartId, string productCartName, CancellationToken cancellationToken);


    }
}
