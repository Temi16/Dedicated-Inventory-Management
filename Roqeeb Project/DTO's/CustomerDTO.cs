using System.Collections.Generic;

namespace Roqeeb_Project.DTO_s
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public IList<OrderDTO> Orders { get; set; }
    }

    public class RegisterCustomerRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }

}
