using Roqeeb_Project.DTO_s;

namespace Roqeeb_Project.View_Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; } 
    }

    public class LoginResponseModel
    { 
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public UserDTO Data { get; set; }
    }
}
