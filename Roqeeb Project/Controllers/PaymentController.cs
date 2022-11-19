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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentRequestModel model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var payment = await _paymentService.MakePayment(model, cancellationToken);
            if (payment.status == false)
            {
                return BadRequest(payment);
            }
            return Ok(payment);
        }

        [HttpPut("VerifyPayment/{referrenceNumber}")]
        public async Task<IActionResult> VerifyPayment([FromRoute] string referrenceNumber, CancellationToken cancellationToken)
        {
            if (referrenceNumber == null)
            {
                return BadRequest();
            }
            var payment = await _paymentService.VerifyPayment(referrenceNumber, cancellationToken);
            if (payment.Status == false)
            {
                return BadRequest(payment);
            }
            return Ok(payment);
        }

        //[HttpGet("GetAllPaymentsByCustomer/{customerId}")]
        //public async Task<IActionResult> GetAllPaymentsByCustomer([FromRoute] string customerId, CancellationToken cancellationToken)
        //{
        //    var payment = await _paymentService.GetAllPaymentsByCustomer(customerId, cancellationToken);
        //    if (payment. == false)
        //    {
        //        return BadRequest(payment);
        //    }
        //    return Ok(payment);
        //}
    }
}
