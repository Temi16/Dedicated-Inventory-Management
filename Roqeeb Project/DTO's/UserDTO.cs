using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<RoleDTO> Roles { get; set; }
    }
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordRequestModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
    public class UpdateUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
