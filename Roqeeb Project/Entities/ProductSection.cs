using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class ProductSection : AuditableEntity
    {
        public string ProductId { get; set; }
        public string SectionId { get; set; }
        public Product Product { get; set; }
        public Section Section { get; set; }
    }
}
