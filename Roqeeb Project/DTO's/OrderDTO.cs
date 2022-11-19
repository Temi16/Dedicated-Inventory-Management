namespace Roqeeb_Project.DTO_s
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public CustomerCartDTO Cart { get; set; }
        public string ReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public double TotalCost { get; set; }
    }
}
