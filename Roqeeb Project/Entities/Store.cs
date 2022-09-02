using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Store : AuditableEntity
    {
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public ICollection<Section> Sections { get; set; }
        
    }
}
