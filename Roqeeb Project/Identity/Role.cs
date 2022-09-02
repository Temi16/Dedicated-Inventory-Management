using System.Collections;
using System.Collections.Generic;
using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Identity
{
    public class Role : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
