using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductSales : AuditableEntity
    {
        public string ProductId { get; set; }
        public string SalesId { get; set; }
        public Product Product { get; set; }
        public Sales Sales { get; set; }
    }
}
