using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IUserService
    {
        Task<BaseResponse<UserDTO>> Login(LoginRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<UserDTO>> ChangePassword(string email, ChangePasswordRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<UserDTO>> UpdateDetails(string userId, UpdateUserRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<UserDTO>> GetUser(string userId, CancellationToken cancellationToken);
        BaseResponse<IList<UserDTO>> GetAllUsers(CancellationToken cancellationToken);
    }
}
