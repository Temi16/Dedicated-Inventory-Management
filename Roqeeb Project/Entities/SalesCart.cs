using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class SalesCart : AuditableEntity
    {
        public bool IsActive { get; set; } = true;
        public double TotalAmount { get; set; }
        public IList<ProductSalesCart> ProductSalesCarts { get; set; }
    }
}
