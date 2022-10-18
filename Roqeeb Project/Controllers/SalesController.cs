using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }
        [HttpPost("CreateSales/{cartId}/{customerName}")]
        public async Task<IActionResult> CreatePurchase([FromRoute] string cartId, [FromRoute] string customerName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sales = await _salesService.Create(cartId, customerName, cancellationToken);
            if (sales.Status == false) return BadRequest(sales.Message);
            return Ok(sales);
        }
        [HttpGet("ViewSalesToday")]
        public async Task<IActionResult> ViewSalesToday(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewSales = await _salesService.ViewSalesToday(cancellationToken);
            //if (sales.Status == false) return BadRequest(sales.Message);
            return Ok(viewSales);
        }
        [HttpGet("ViewSalesThisWeek")]
        public async Task<IActionResult> ViewSalesThisWeek(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewSales = await _salesService.ViewSalesThisWeek(cancellationToken);
            return Ok(viewSales);
        }
        [HttpGet("ViewSalesThisMonth")]
        public async Task<IActionResult> ViewSalesThisMonth(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewSales = await _salesService.ViewSalesThisMonth(cancellationToken);
            return Ok(viewSales);
        }
        [HttpGet("ViewAllSales")]
        public async Task<IActionResult> ViewAllSales(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewSales = await _salesService.GetAll(cancellationToken);
            if (viewSales.Status == false) return BadRequest(viewSales.Message);
            return Ok(viewSales);
        }
        [HttpGet("ViewSalesByDate/{date}")]
        public async Task<IActionResult> ViewSalesByDate([FromRoute]string date, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var viewSales = await _salesService.GetByDate(date, cancellationToken);
            if (viewSales.Status == false) return Ok(viewSales);
            return Ok(viewSales);
        }
    }
}
