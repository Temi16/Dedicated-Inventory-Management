using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        [HttpPost("CreatePurchase")]
        public async Task<IActionResult> CreatePurchase([FromForm]CreatePurchaseRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _purchaseService.Create(request, cancellationToken);
            if (purchase.Status == false) return BadRequest(purchase.Message);
            return Ok(purchase);
        }
    }
}
