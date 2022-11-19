using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface ICustomerService
    {
        Task<BaseResponse<CustomerDTO>> CreateCustomer(RegisterCustomerRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<IEnumerable<CustomerDTO>>> ViewAllCustomers(CancellationToken cancellationToken);
    }
}
