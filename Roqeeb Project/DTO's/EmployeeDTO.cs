namespace Roqeeb_Project.DTO_s
{
    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateEmployeeRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class ConfirmEmployeeRequestModel
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class UpdateEmployeeRequestModel
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
