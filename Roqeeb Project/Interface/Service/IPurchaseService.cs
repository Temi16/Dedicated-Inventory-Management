using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IPurchaseService
    {
        Task<BaseResponse<PurchaseDTO>> Create(CreatePurchaseRequestModel request, CancellationToken cancellationToken);
    }
}
