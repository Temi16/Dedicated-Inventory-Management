using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.RequestModels.StoreRequestModels;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpPost("CreateStore")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateStoreRequestModel request, CancellationToken cancellationToken)
        {
            var store = await _storeService.Create(request, cancellationToken);
            if (store.Status == false) return BadRequest(store.Message);
            return Ok(store);
        }
        [HttpGet("AllStores")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var stores = await _storeService.GetAllStore(cancellationToken);
            if (stores.Status == false) return BadRequest(stores);
            return Ok(stores);
        }
    }
}
