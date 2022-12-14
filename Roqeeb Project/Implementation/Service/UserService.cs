using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.Auth.Service;
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
        private readonly IIdentityService _identityService;
        private readonly IQueryableUserStore<User> _users;


        public UserService(IUserStore<User> userRepository, IUserPasswordStore<User> userPasswordRepository, IUserRoleStore<User> userRoleRepository, IUserEmailStore<User> userEmailRepository, IIdentityService identityService, IQueryableUserStore<User> users)
        {
            _userRepository = userRepository;
            _userPasswordRepository = userPasswordRepository;
            _userEmailRepository = userEmailRepository;
            _userRoleRepository = userRoleRepository;
            _identityService = identityService;
            _users = users;
        }

        public async Task<BaseResponse<UserDTO>> ChangePassword(string email, ChangePasswordRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userEmailRepository.FindByEmailAsync(email, cancellationToken);
            if(user == null)
            {
                return new BaseResponse<UserDTO>
                {
                    Message = "Email is incorrect",
                    Status = false
                };
            };
            var oldPassword = $"{request.oldPassword}{user.Salt}";
            if(user.Password
                .Equals(oldPassword))
            {
                user.Password = $"{request.newPassword}{user.Salt}";
                await _userRepository.UpdateAsync(user, cancellationToken);
                return new BaseResponse<UserDTO>
                {
                    Data = new UserDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.Username,
                    },
                    Message = "Successfull",
                    Status = true
                }; 
            }
            else
            {
                return new BaseResponse<UserDTO>
                {
                    Message = "Incorrect Password",
                    Status = false
                };
            }
            
        }

        public BaseResponse<IList<UserDTO>> GetAllUsers(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var users = _users.Users.AsQueryable();
            if (users == null) return new BaseResponse<IList<UserDTO>>
            {
                Message = "user not found",
                Status = false
            };
            return new BaseResponse<IList<UserDTO>>
            {
              
                Data = users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserName = u.Username
                }).ToList(),
                Message = "users Found",
                Status = true,
            };
        }

        public async Task<BaseResponse<UserDTO>> GetUser(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null) return new BaseResponse<UserDTO>
            {
                Message = "user not found",
                Status = false
            };
            return new BaseResponse<UserDTO>
            {
                Data = new UserDTO
                {
                    Id=user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.Username
                },
                Message = "user Found",
                Status = true
            };
        }

        public async Task<BaseResponse<UserDTO>> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userEmailRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (user == null) return new BaseResponse<UserDTO>
            {
                Message = "Failed",
                Status = false
            };
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
        public async Task<BaseResponse<UserDTO>> UpdateDetails(string userId, UpdateUserRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);
            if (user == null) return new BaseResponse<UserDTO>
            {
                Message = "user Not Found",
                Status = false
            };
            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.Username = request.Username ?? user.Username;
            user.Email = request.Email ?? user.Email;
            user.LastModifiedOn = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, cancellationToken);
            return new BaseResponse<UserDTO>
            {
                Data = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                Message = "User Successfully Updated",
                Status = true

            };
        }
    }
}
