using System.Collections.Generic;
using Roqeeb_Project.Contract;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Entities
{
    public class Customer : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
