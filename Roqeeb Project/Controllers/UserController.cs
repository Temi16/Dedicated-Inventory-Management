
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        public UserController(IIdentityService identityService, IUserService userService)
        {
            _identityService = identityService;
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromForm]LoginRequestModel request, CancellationToken cancellatioToken)
        {
            cancellatioToken.ThrowIfCancellationRequested();
            var create = await _userService.Login(request, cancellatioToken);
            if(create.Status == false) return BadRequest(create);
            IList<string> roles = new List<string>();
            foreach(var role in create.Data.Roles)
            {
                roles.Add(role.Name);
            }
            var token = _identityService.GenerateToken(create.Data, roles);
            var loginResponse = new LoginResponseModel
            {
                Data = create.Data,
                Message = "Login successful",
                Status = true,
                Token = token
            };
            return Ok(loginResponse);
            
        }
    }
}
