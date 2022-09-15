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
        Task<BaseResponse<AdminCartDTO>> AddToCart(string productId, string adminCartId, CancellationToken cancellationToken);
        Task<BaseResponse<AdminCartDTO>> UpdateCart(string cartId, CancellationToken cancellationToken);    
    }
}
