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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost("CustomerRegistration")]
        public async Task<IActionResult> RegisterCustomer([FromForm] RegisterCustomerRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var customer = await _customerService.CreateCustomer(request, cancellationToken);
            if (customer.Status == false) return BadRequest(customer);
            return Ok(customer);
        }
        [HttpGet("ViewAllCustomers")]
        public async Task<IActionResult> ViewAllCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerService.ViewAllCustomers(cancellationToken);
            if (customers.Status == false) return BadRequest(customers);
            return Ok(customers);
        }
    }
}
