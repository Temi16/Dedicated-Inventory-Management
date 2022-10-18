using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Supplier : AuditableEntity
    {
        public string SupplierName { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
