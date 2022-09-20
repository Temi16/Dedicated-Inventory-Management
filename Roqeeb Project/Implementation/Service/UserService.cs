using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<UserDTO>> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passWordHash = await _userRepository.GetPasswordHashAsync(user, cancellationToken);
            List<Role> roles = new List<Role>();
            foreach (var role in user.UserRoles)
            {
                roles.Add(role.Role);
            }
            if (user != null && $"{user.Password}{user.Salt}".Equals(passWordHash)) return new BaseResponse<UserDTO>
            {
                Data = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.Username,
                    Email = user.Email,
                    Roles = roles.Select(r => new RoleDTO
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList()
                },
                Message = "Login Successful",
                Status = true
            };
            return new BaseResponse<UserDTO>
            {
                Message = "Incorrect Username or Password",
                Status = false
            };
        }

    }
}
