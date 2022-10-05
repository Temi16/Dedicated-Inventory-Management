using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IEmployeeService
    {
        Task<BaseResponse<EmployeeDTO>> CreateEmployee(CreateEmployeeRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<EmployeeDTO>> ConfirmEmployee(ConfirmEmployeeRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<EmployeeDTO>> UpdateEmployee(UpdateEmployeeRequestModel request, CancellationToken cancellationToken);
    }
}
