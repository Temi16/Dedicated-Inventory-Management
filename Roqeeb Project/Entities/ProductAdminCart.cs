using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductAdminCart : AuditableEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string AdminCartId { get; set; }
        public AdminCart AdminCart { get; set; }
    }
}
