using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Sales : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string SalesCartId { get; set; }
        public SalesCart SalesCart { get; set; }
        public ICollection<ProductSales> ProductSales { get; set; } = new HashSet<ProductSales>();
    }
}
