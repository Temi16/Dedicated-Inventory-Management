using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Auth.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserRepository _userRepository;

        public IdentityService(IHttpContextAccessor context, IConfiguration configuration, IPasswordHasher<User> passwordHasher, UserRepository userRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public string GenerateToken(UserDTO user, IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtTokenSettings : TokenKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)

            }; 
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(_configuration.GetValue<string>("JwtTokenSettings:TokenIssuer"),
               _configuration.GetValue<string>("JwtTokenSettings:TokenIssuer"),
               claims,
               DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetValue<string>("JwtTokenSettings:TokenExpiryPeriod"))),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public JwtSecurityToken GetClaims(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                if(token.StartsWith("B"))
                {
                    token = token.Split(" ")[1];
                }
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
                return decodedToken;
            }
            return null;
        }


        public string GetClaimValue(string type)
        {
            return _context.HttpContext.User.FindFirst(type).Value;
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
        public string GenerateSalt()
        {
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[10];
            cryptoServiceProvider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
        public string PassWordHash(string password, string salt = null)
        {
            if(string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(new User(), password);
            }
            return _passwordHasher.HashPassword(new User(), $"{password}{salt}");
        }

        public async Task<BaseResponse<UserDTO>> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            var passWordHash = await _userRepository.GetPasswordHashAsync(user, cancellationToken);
            List<Role> roles = new List<Role>();
            foreach(var role in user.UserRoles)
            {
                roles.Add(role.Role);
            }
            if (user != null && $"{user.Password}{user.Salt}" == passWordHash) return new BaseResponse<UserDTO>
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
