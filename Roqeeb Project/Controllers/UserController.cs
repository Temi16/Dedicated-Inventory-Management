
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(LoginRequestModel request, CancellationToken cancellatioToken)
        {
            cancellatioToken.ThrowIfCancellationRequested();
            var create = await _identityService.Login(request, cancellatioToken);
            if(create.Status == false) return BadRequest(create);
            IList<string> roles = new List<string>();
            foreach(var role in create.Data.Roles)
            {
                roles.Add(role.Name);
            }
            _identityService.GenerateToken(create.Data, roles);
            return Ok(create);
            
        }
    }
}
