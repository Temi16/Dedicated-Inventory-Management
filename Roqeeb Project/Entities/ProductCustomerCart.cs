using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductCustomerCart : AuditableEntity
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double Price { get; set; }
        public string CustomerCartId { get; set; }
        public CustomerCart CustomerCart { get; set; }
    }
}
