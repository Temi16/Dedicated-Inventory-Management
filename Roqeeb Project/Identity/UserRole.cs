using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Identity
{
    public class UserRole : AuditableEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
