namespace Roqeeb_Project.View_Models.RequestModels.ProductRequestMosels
{
    public class UpdateProductRequestModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; } 
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public string Image { get; set; }
    }
}
