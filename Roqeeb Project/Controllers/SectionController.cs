using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels.SectionRequestModels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }
        [HttpPost("CreateSection")]
        public async Task<IActionResult> CreateSection([FromForm] CreateSectionRequestModel request, CancellationToken cancellationToken)
        {
            var section = await _sectionService.CreateSection(request, cancellationToken);
            if (section.Status == false) return BadRequest(section.Message);
            return Ok(section);
        }
    }
}
