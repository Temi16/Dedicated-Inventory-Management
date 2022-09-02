namespace Roqeeb_Project.View_Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; } 
    }
}
