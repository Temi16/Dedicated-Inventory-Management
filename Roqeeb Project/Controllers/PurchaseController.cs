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
        [HttpPost("CreatePurchase/{cartId}/{supplierId}")]
        public async Task<IActionResult> CreatePurchase([FromRoute]string cartId, [FromRoute]string supplierId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _purchaseService.Create(cartId, supplierId, cancellationToken);
            if (purchase.Status == false) return BadRequest(purchase.Message);
            return Ok(purchase);
        }
        [HttpGet("PendingPurchase")]
        public async Task<IActionResult> PendingPurchase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchases = await _purchaseService.GetNonApprovedPurchase(cancellationToken);
            if (purchases.Status == false) return BadRequest(purchases);
            return Ok(purchases);
        }
        [HttpGet("ApprovedPurchase")]
        public async Task<IActionResult> ApprovedPurchase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchases = await _purchaseService.GetAllApprovedPurchase(cancellationToken);
            if (purchases.Status == false) return BadRequest(purchases);
            return Ok(purchases);
        }
        [HttpPost("ApprovePurchase/{purchaseId}")]
        public async Task<IActionResult> ApprovePurchase([FromRoute]string purchaseId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var purchase = await _purchaseService.ApprovePurchase(purchaseId, cancellationToken);
            if (purchase.Status == false) return BadRequest(purchase);
            return Ok(purchase);
        }
        [HttpGet("ViewPurchaseByDate/{date}")]
        public async Task<IActionResult> ViewPurchaseByDate([FromRoute] string date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewPurchase = await _purchaseService.GetByDate(date, cancellationToken);
            if (viewPurchase.Status == false) return BadRequest(viewPurchase);
            return Ok(viewPurchase);
        }
    }
}
