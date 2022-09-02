using System;

namespace Roqeeb_Project.Contract
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
