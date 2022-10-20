using Roqeeb_Project.Contract;

namespace Roqeeb_Project.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
