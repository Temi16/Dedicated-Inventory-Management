using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ISupplierService
    {
        Task<BaseResponse<SupplierDTO>> CreateSupplier(CreateSupplierRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<IList<SupplierDTO>>> GetAllSuppliers(CancellationToken cancellationToken);
    }
}
