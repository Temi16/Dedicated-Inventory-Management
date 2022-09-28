using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductCart : AuditableEntity
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string AdminCartId { get; set; }
        public AdminCart AdminCart { get; set; }
    }
}
