using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Sales : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public ICollection<ProductSales> Products { get; set; } = new HashSet<ProductSales>();
    }
}
