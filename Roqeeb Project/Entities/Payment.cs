using Roqeeb_Project.Contract;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Entities
{
    public class Payment : AuditableEntity
    {
        public string ReferrenceNumber { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal AmountPaid { get; set; }
        public string DateOfPayment { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public bool IsVerified { get; set; }
    }
}
