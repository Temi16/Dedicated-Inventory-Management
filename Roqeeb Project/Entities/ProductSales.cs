using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductSales : AuditableEntity
    {
        public int ProductId { get; set; }
        public int SalesId { get; set; }
        public Product Product { get; set; }
        public Sales Sales { get; set; }
    }
}
