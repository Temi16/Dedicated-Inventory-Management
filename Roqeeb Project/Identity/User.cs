using System.Collections.Generic;
using Roqeeb_Project.Contract;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Identity
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Salt { get; set; }
        public Admin Admin { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
