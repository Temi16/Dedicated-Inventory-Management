using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Identity;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Auth.Service
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        string GenerateToken(UserDTO user, IList<string> roles);
        JwtSecurityToken GetClaims(string token);
        string GetClaimValue(string type);
        string GenerateSalt();
        string PassWordHash(string password, string salt = null);
        


    }
}
