using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Section : AuditableEntity
    {
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public string StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<ProductSection> productSections { get; set; } = new HashSet<ProductSection>();
    }
}
