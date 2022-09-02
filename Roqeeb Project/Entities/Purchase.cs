using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Purchase : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public ICollection<ProductSales> ProductSales { get; set; } = new HashSet<ProductSales>();
    }
}
