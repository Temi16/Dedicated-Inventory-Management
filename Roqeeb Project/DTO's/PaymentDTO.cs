using System.Collections.Generic;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.DTO_s
{
    public class PaymentDTO
    {
        public string Id { get; set; }
        public string ReferrenceNumber { get; set; }
        public string Username { get; set; }
        public decimal AmountPaidByCustomer { get; set; }
        public string DateOfPayment { get; set; }
    }
    public class PaymentRequestModel
    {
        public string UserId { get; set; }
        public string CartId { get; set; }
        public decimal AmountPaid { get; set; }
    }
    public class SendMoneyDTO
    {
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public decimal Amount { get; set; }
    }
    public class PaymentsResponseModel
    {
        public IList<PaymentDTO> Data { get; set; } = new List<PaymentDTO>();
    }
}
