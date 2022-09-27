using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly IUserStore<User> _userRepository;
        private readonly IUserPasswordStore<User> _userPasswordRepository;
        private readonly IUserRoleStore<User> _userRoleRepository;
        private readonly IUserEmailStore<User> _userEmailRepository;
      

        public UserService(IUserStore<User> userRepository, IUserPasswordStore<User> userPasswordRepository, IUserRoleStore<User> userRoleRepository, IUserEmailStore<User> userEmailRepository)
        {
            _userRepository = userRepository;
            _userPasswordRepository = userPasswordRepository;
            _userEmailRepository = userEmailRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<BaseResponse<UserDTO>> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userEmailRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passWordHash = await _userPasswordRepository.GetPasswordHashAsync(user, cancellationToken);
            var requestPassword = $"{request.Password}{user.Salt}";
            List<Role> roles = new List<Role>();
            foreach (var role in user.UserRoles)
            {
                roles.Add(role.Role);
            }
            if (user != null &&  passWordHash.Equals(requestPassword)) return new BaseResponse<UserDTO>
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
