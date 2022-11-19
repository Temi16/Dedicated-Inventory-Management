using Roqeeb_Project.Contract;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Entities
{
    public class Order : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public string CustomerCartId { get; set; }
        public CustomerCart CustomerCart { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsApproved { get; set; }
    }
}
