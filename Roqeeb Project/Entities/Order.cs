using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Order : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
