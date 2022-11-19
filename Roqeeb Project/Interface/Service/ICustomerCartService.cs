using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ICustomerCartService
    {
        Task<BaseResponse<CustomerCartDTO>> CreateCart(string userId, CancellationToken cancellationToken);
        Task<BaseResponse<CustomerCartDTO>> GetById(string cartId, CancellationToken cancellationToken);
        Task<BaseResponse<CustomerCartDTO>> GetByStatus(string userId, CancellationToken cancellationToken);
        Task<BaseResponse<CustomerCartDTO>> AddToCart(AddOrdersToCartRequestModel request, string customerCartId, CancellationToken cancellationToken);
        Task<BaseResponse<CustomerCartDTO>> DeleteUpdateCart(string cartId, string productName, CancellationToken cancellationToken);
        Task<BaseResponse<CustomerCartDTO>> DeleteUpdateAllCart(string cartId, CancellationToken cancellationToken);
    }
}
