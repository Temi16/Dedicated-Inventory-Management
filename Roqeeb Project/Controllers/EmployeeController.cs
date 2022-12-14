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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromForm]CreateEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeService.CreateEmployee(request, cancellationToken);
            if (employee.Status == false) return BadRequest(employee);
            return Ok(employee);
        }
        [HttpPost("ConfirmEmployee")]
        public async Task<IActionResult> VerifyEmployee([FromForm] ConfirmEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeService.ConfirmEmployee(request, cancellationToken);
            if (employee.Status == false) return BadRequest(employee);
            return Ok(employee);
        }
        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeService.UpdateEmployee(request, cancellationToken);
            if (employee.Status == false) return BadRequest(employee);
            return Ok(employee);
        }
        [HttpGet("ViewAllEmployees")]
        public async Task<IActionResult> ViewAllEmployees(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.ViewAllEmployees(cancellationToken);
            if (employees.Status == false) return BadRequest(employees);
            return Ok(employees);
        }
        [HttpGet("ViewProductDetails")]
        public async Task<IActionResult> ViewProductDetails(CancellationToken cancellationToken)
        {
            var products = await _employeeService.ProductDetails(cancellationToken);
            if (products.Status == false) return BadRequest(products);
            return Ok(products);
        }
    }
}
