using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ISalesCartService
    {
        Task<BaseResponse<SalesCartDTO>> CreateCart(CancellationToken cancellationToken);
        Task<BaseResponse<SalesCartDTO>> GetById(string cartId, CancellationToken cancellationToken);
        Task<BaseResponse<SalesCartDTO>> GetByStatus(CancellationToken cancellationToken);
        Task<BaseResponse<SalesCartDTO>> AddToCart(AddSalesToCartRequestModel request, string adminCartId, CancellationToken cancellationToken);
        Task<BaseResponse<SalesCartDTO>> EditCart(string cartId, string ProductName, EditProductSalesCart request, CancellationToken cancellationToken);
        Task<BaseResponse<SalesCartDTO>> DeleteUpdateCart(string cartId, string productName, CancellationToken cancellationToken);
    }
}
