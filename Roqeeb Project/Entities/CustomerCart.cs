using System.Collections.Generic;
using Roqeeb_Project.Contract;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Entities
{
    public class CustomerCart : AuditableEntity
    {
        public bool IsActive { get; set; } = true;
        public double TotalAmount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public IList<ProductCustomerCart> ProductCustomerCarts { get; set; }
    }
}
