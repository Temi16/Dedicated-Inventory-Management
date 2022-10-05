using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
           _supplierService = supplierService;
        }
        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier([FromForm] CreateSupplierRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier = await _supplierService.CreateSupplier(request, cancellationToken);
            if (supplier.Status == false) return BadRequest(supplier);
            return Ok(supplier);
        }
        [HttpGet("AllSuppliers")]
        public async Task<IActionResult> AllSuppliers(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var suppliers = await _supplierService.GetAllSuppliers(cancellationToken);
            if (suppliers.Status == false) return BadRequest(suppliers);
            return Ok(suppliers);
        }
    }
}
