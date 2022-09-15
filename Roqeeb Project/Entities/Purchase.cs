using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Purchase : AuditableEntity
    {
        public string ReferenceNo { get; set; }
        public string AdminCartId { get; set; }
        public AdminCart AdminCart { get; set; }
        
       
    }
}
