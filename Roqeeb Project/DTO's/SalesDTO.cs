namespace Roqeeb_Project.DTO_s
{
    public class SalesDTO
    {
        public string Id { get; set; }
        public SalesCartDTO Cart { get; set; }
        public string ReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public double TotalCost { get; set; }
    }

    public class ProductAndQuantity
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
