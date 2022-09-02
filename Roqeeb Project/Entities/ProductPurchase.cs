using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductPurchase : AuditableEntity
    {
        public int ProductId { get; set; }
        public int PurchaseId { get; set; }
        public Product Product { get; set; }
        public Purchase Purchase { get; set; }
    }
}
