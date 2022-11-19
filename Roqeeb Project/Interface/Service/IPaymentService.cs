using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface IPaymentService
    {
        Task<PaystackResponse> MakePayment(PaymentRequestModel model, CancellationToken cancellationToken);

        Task<BaseResponse<PaymentDTO>> VerifyPayment(string referrenceNumber, CancellationToken cancellationToken);

        Task<PaymentsResponseModel> GetAllPaymentsByCustomer(string customerId, CancellationToken cancellationToken);
    }
}
