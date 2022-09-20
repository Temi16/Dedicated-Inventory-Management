using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class AdminCart : AuditableEntity
    {
        public bool IsActive { get; set; } = true;
        public double TotalAmount { get; set; }
        public IList<ProductAdminCart> ProductAdminsCart { get; set; }
        public ICollection<ProductCart> productCarts { get; set; } = new List<ProductCart>();
        
    }
}
